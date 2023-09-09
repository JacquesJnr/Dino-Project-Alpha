using System;

using UnityEngine;

public class FloorTiler : MonoBehaviour
{
    public MeshRenderer floorPrefab;
    public Transform floorRoot;
    public int numTiles = 5;
    public float floorY;
    public float speed = 5f;
    public float wrapPosition = -25f;

    public Material[] tileMaterials;

    const float PLANE_SIZE_UNITS = 10f;
    private float _tileSize;

    [NonSerialized] public MeshRenderer[] tiles;

    private void Start()
    {
        _tileSize = floorPrefab.transform.localScale.z*PLANE_SIZE_UNITS;

        foreach(Transform t in floorRoot)
        {
            Destroy(t.gameObject);
        }

        tiles = new MeshRenderer[numTiles];
        for(int i = 0; i < numTiles; i++)
        {
            MeshRenderer tile = Instantiate(floorPrefab, floorRoot);
            tile.sharedMaterial = tileMaterials.Choose();
            tile.transform.position = new Vector3(0f, floorY, _tileSize*i);
            tiles[i] = tile;
        }    
    }

    private void Update()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            MeshRenderer tile = tiles[i];
            Vector3 pos = tile.transform.position;

            pos.z -= speed*Time.deltaTime;
            if(pos.z <= wrapPosition)
            {
                tile.sharedMaterial = tileMaterials.Choose();
                pos.z += tiles.Length*_tileSize;
            }

            tile.transform.position = pos;
        }
    }
}
