using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    public int waveSize = 3;
    public float waveDelay = 10f;
    public float distance = 100f;
    public float obstacleDistance = 15f;

    private int _currentWave;
    private float _lastWaveTime = -999f;
    private PlayerController Player => PlayerController.Instance;

    void Update()
    {
        if(Time.time > _lastWaveTime + waveDelay)
        {
            _currentWave++;
            _lastWaveTime = Time.time;
            for(int i = 0; i < waveSize; i++)
            {
                Vector3 pos = Vector3.forward*(distance + obstacleDistance*i);
                pos.x = Random.Range(-1, 2)*Player.laneWidth;
                GameObject obstacle = Instantiate(obstaclePrefabs.Choose(), pos, Quaternion.identity);
            }
        }
    }
}
