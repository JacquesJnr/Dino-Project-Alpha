using System;
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
        foreach(Transform t in floorRoot)
        {
            Destroy(t.gameObject);
        }

        tiles = new LevelTile[numTiles];
        for(int i = 0; i < numTiles; i++)
        {
            LevelTile prefab = tilePrefabs.Choose(out int tileIndex);
            LevelTile tile = Instantiate(prefab, floorRoot);
            tile.transform.localEulerAngles = tileRotation;
            tile.transform.position = new Vector3(floorX, floorY, _lastTilePos);
            _lastTilePos += tile.tileSize;
            tiles[i] = tile;
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
            if(pos.z <= wrapPosition)
            {
                Destroy(tile);
                LevelTile prefab;
                int tileIndex;

                currentModeTiles++;
                if(currentModeTiles > currentModeLength)
                {
                    currentModeLength = UnityEngine.Random.Range(minModeLength, maxModeLength + 1);
                    currentModeTiles = 0;
                    Mode[] possibleModes = ((Mode[])Enum.GetValues(typeof(Mode))).Where(x => x != currentMode).ToArray();
                    Mode newMode = possibleModes.Choose();
                    prefab = GetTilesForModeSwap(currentMode, newMode).Choose(out tileIndex);
                    currentMode = newMode;
                }
                else
                {
                    prefab = GetTilesForMode(currentMode).Choose(out tileIndex);
                }
                tile = Instantiate(prefab, floorRoot);
                tile.transform.localEulerAngles = tileRotation;
                tiles[i] = tile;
                pos.z = _lastTilePos;
                _lastTilePos += tile.tileSize;
            }

            tile.transform.position = pos;
        }
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