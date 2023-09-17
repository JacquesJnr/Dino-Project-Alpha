using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

using UnityEngine;

public class Tiler : MonoBehaviour
{
    public LevelTile[] tilePrefabs;
    public LevelTile[] rollingTilePrefabs;
    public LevelTile[] flyingTilePrefabs;
    public LevelTile[] runningToRollingTiles;
    public LevelTile[] runningToFlyingTiles;
    public LevelTile[] rollingToRunningTiles;
    public LevelTile[] rollingToFlyingTiles;
    public LevelTile[] flyingToRunningTiles;
    public LevelTile[] flyingToRollingTiles;

    public Transform floorRoot;
    public int numTiles = 5;
    public float floorY;
    public float floorX;
    public float wrapPosition = -25f;
    public Vector3 tileRotation;
    public int minModeLength = 10;
    public int maxModeLength = 20;

    private Mode currentMode = Mode.Running;
    private int currentModeTiles;
    private int currentModeLength;

    [NonSerialized] public LevelTile[] tiles;
    [SerializeField] private float _lastTilePos;

    private void Start()
    {
        currentModeLength = maxModeLength;

        foreach(Transform t in floorRoot)
        {
            Destroy(t.gameObject);
        }

        tiles = new LevelTile[numTiles];
        for(int i = 0; i < numTiles; i++)
        {
            tiles[i] = CreateTile();
        }    
    }

    private void Update()
    {
        float speed = GamePhases.Instance.activePhase.tileSpeed*Time.deltaTime*PlayerController.Instance.DashSpeedMultipiplier;
        _lastTilePos -= speed;

        for(int i = 0; i < tiles.Length; i++)
        {
            LevelTile tile = tiles[i];

            Vector3 pos = tile.transform.position;
            pos.z -= speed;
            tile.transform.position = pos;

            if(pos.z <= wrapPosition)
            {
                Destroy(tile.gameObject);
                tiles[i] = CreateTile();
            }
        }
    }

    private LevelTile CreateTile()
    {
        currentModeTiles++;
        LevelTile[] possibleTiles;
        if(currentModeTiles > currentModeLength)
        {
            currentModeLength = UnityEngine.Random.Range(minModeLength, maxModeLength + 1);
            currentModeTiles = 0;

            Mode[] possibleModes = ((Mode[])Enum.GetValues(typeof(Mode))).Where(x => x != currentMode).ToArray();
            Mode newMode = possibleModes.Choose();
            possibleTiles = GetTilesForModeSwap(currentMode, newMode);
            currentMode = newMode;
        }
        else
        {
             possibleTiles = GetTilesForMode(currentMode);
        }

        Dictionary<LevelTile, int> tileWeights = new();
        foreach(var t in possibleTiles)
        {
            int weight = t.GetSpawnWeight();
            if(weight <= 0) continue;
            tileWeights.Add(t, weight);
        }

        Distribution<LevelTile> tileDistribution = new Distribution<LevelTile>(tileWeights);
        LevelTile prefab = tileDistribution.NextValue();

        LevelTile tile = Instantiate(prefab, floorRoot);
        tile.transform.localEulerAngles = tileRotation;
        tile.transform.localPosition = new Vector3(0f, 0f, _lastTilePos);
        _lastTilePos += tile.tileSize;

        return tile;
    }

    private LevelTile[] GetTilesForMode(Mode mode)
    {
        switch(mode)
        {
            case Mode.Running: return tilePrefabs;
            case Mode.Flying: return flyingTilePrefabs;
            case Mode.Rolling: return rollingTilePrefabs;
            default:
                throw new Exception($"No tiles defined for mode '{mode}'");
        }
    }

    private LevelTile[] GetTilesForModeSwap(Mode from, Mode to)
    {
        if(from == Mode.Running && to == Mode.Flying)
        {
            return runningToFlyingTiles;
        }
        else if(from == Mode.Running && to == Mode.Rolling)
        {
            return runningToRollingTiles;
        }
        else if(from == Mode.Rolling && to == Mode.Running)
        {
            return rollingToRunningTiles;
        }
        else if(from == Mode.Rolling && to == Mode.Flying)
        {
            return rollingToFlyingTiles;
        }
        else if(from == Mode.Flying && to == Mode.Running)
        {
            return flyingToRunningTiles;
        }
        else if(from == Mode.Flying && to == Mode.Rolling)
        {
            return flyingToRollingTiles;
        }

        throw new Exception($"No tiles defined for mode swap from '{from}' to '{to}'");
    }
}