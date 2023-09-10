using System;

using UnityEngine;

public class Tiler : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public Transform floorRoot;
    public int numTiles = 5;
    public float floorY;
    public float floorX;
    public float wrapPosition = -25f;
    public Vector3 tileRotation;

    const float PLANE_SIZE_UNITS = 10f;
    private float _tileSize;

    [NonSerialized] public GameObject[] tiles;

    private void Start()
    {
        _tileSize = tilePrefabs[0].transform.localScale.x*PLANE_SIZE_UNITS;

        foreach(Transform t in floorRoot)
        {
            Destroy(t.gameObject);
        }

        tiles = new GameObject[numTiles];
        for(int i = 0; i < numTiles; i++)
        {
            GameObject prefab = tilePrefabs.Choose();
            GameObject tile = Instantiate(prefab, floorRoot);
            tile.transform.localEulerAngles = tileRotation;
            tile.transform.position = new Vector3(floorX, floorY, _tileSize*i);
            tiles[i] = tile;
        }    
    }

    private void Update()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            GameObject tile = tiles[i];
            Vector3 pos = tile.transform.position;

            pos.z -= PlayerController.Instance.speed*Time.deltaTime;
            if(pos.z <= wrapPosition)
            {
                Destroy(tile);
                GameObject prefab = tilePrefabs.Choose();
                tile = Instantiate(prefab, floorRoot);
                tile.transform.localEulerAngles = tileRotation;
                tiles[i] = tile;
            }

            tile.transform.position = pos;
        }
    }
}
