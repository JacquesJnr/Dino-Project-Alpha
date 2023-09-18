using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    public float tileSize = 30f;
    public float spawnWeight = 1f;
    public float spawnWeightPerSecond = 0.01f;
    public float maxSpawnWeight = 1f;

    public int GetSpawnWeight()
    {
        return Mathf.RoundToInt(100f*Mathf.Clamp(spawnWeight + spawnWeightPerSecond*Time.timeSinceLevelLoad, 0f, maxSpawnWeight));
    }
}
