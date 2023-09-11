using System;

using UnityEngine;

public class Tiler : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float[] tileSizes;
    public Transform floorRoot;
    public int numTiles = 5;
    public float floorY;
    public float floorX;
    public float wrapPosition = -25f;
    public Vector3 tileRotation;

    [NonSerialized] public GameObject[] tiles;
    [SerializeField] private float _lastTilePos;

    private void Start()
    {
        foreach(Transform t in floorRoot)
        {
            Destroy(t.gameObject);
        }

        tiles = new GameObject[numTiles];
        for(int i = 0; i < numTiles; i++)
        {
            GameObject prefab = tilePrefabs.Choose(out int tileIndex);
            GameObject tile = Instantiate(prefab, floorRoot);
            tile.transform.localEulerAngles = tileRotation;
            tile.transform.position = new Vector3(floorX, floorY, tileSizes[tileIndex]*i);
            _lastTilePos += tileSizes[tileIndex];
            tiles[i] = tile;
        }    
    }

    private void Update()
    {
        float speed = PlayerController.Instance.speed*Time.deltaTime;
        _lastTilePos -= speed;
        for(int i = 0; i < tiles.Length; i++)
        {
            GameObject tile = tiles[i];
            Vector3 pos = tile.transform.position;

            pos.z -= speed;
            if(pos.z <= wrapPosition)
            {
                Destroy(tile);
                GameObject prefab = tilePrefabs.Choose(out int tileIndex);
                tile = Instantiate(prefab, floorRoot);
                tile.transform.localEulerAngles = tileRotation;
                tiles[i] = tile;
                pos.z = _lastTilePos;
                _lastTilePos += tileSizes[tileIndex];
            }

            tile.transform.position = pos;
        }
    }
}
