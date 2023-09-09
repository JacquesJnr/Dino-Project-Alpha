using System;

using UnityEngine;

public class FloorTiler : MonoBehaviour
{
    public GameObject floorPrefab;
    public Transform floorRoot;
    public int numTiles = 5;
    public float floorY;
    public float speed = 5f;
    public float wrapPosition = -25f;

    const float PLANE_SIZE_UNITS = 10f;
    private float _tileSize;

    [NonSerialized] public GameObject[] tiles;

    private void Start()
    {
        _tileSize = floorPrefab.transform.localScale.z*PLANE_SIZE_UNITS;

        foreach(Transform t in floorRoot)
        {
            Destroy(t.gameObject);
        }

        tiles = new GameObject[numTiles];
        for(int i = 0; i < numTiles; i++)
        {
            GameObject tile = Instantiate(floorPrefab, floorRoot);
            tile.transform.position = new Vector3(0f, floorY, _tileSize*i);
            tiles[i] = tile;
        }    
    }

    private void Update()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            Transform tile = tiles[i].transform;
            Vector3 pos = tile.position;
            pos.z -= speed*Time.deltaTime;
            if(pos.z <= wrapPosition)
            {
                pos.z += tiles.Length*_tileSize;
            }
            tile.position = pos;
        }
    }
}
