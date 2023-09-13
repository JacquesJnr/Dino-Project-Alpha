using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public int[] lanes;

    public int waveSizeMin = 3;
    public int waveSizeMax = 5;
    public float waveDelay = 10f;
    public float spawnDelay = 1f;
    public float distance = 100f;
    public float obstacleDistance = 15f;

    private int _currentWave;
    private float _lastWaveTime = -999f;
    private PlayerController Player => PlayerController.Instance;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        int waveSize;

        while(true)
        {
            _currentWave++;
            _lastWaveTime = Time.time;
            waveSize = Random.Range(waveSizeMin, waveSizeMax + 1);
            for(int i = 0; i < waveSize; i++)
            {
                Vector3 pos = Vector3.forward*(distance + obstacleDistance*i);
                pos.x = lanes.Choose()*Player.laneWidth;
                GameObject obstacle = Instantiate(obstaclePrefabs.Choose(), pos, Quaternion.identity, transform);
                yield return new WaitForSeconds(spawnDelay);
            }

            yield return new WaitForSeconds(waveDelay);
        }
    }
}
