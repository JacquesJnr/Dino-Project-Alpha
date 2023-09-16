using System;
using System.Collections;

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public int maxHealth = 3;
    [SerializeField] private float hitFreezeDuration = 0.2f;
    [SerializeField] private float hitSlowFactor = 0.5f;
    [SerializeField] public float hitSlowDuration = 0.5f;
    public int Health { get; private set; }

    private float _lastHit = -999;
    public static event Action OnPlayerHit; 

    void Start()
    {
        Instance = this;
        Health = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Obstacle":
                _lastHit = Time.time;
                Health -= 1;
                StartCoroutine(FreezeGame(hitFreezeDuration));
                Debug.Log("Player Hit " + other.name);
                OnPlayerHit?.Invoke();
                break;
        }
    }
    
    private IEnumerator FreezeGame(float time)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
    }

    public float GetSlowFactor()
    {
        if(Time.time > _lastHit + hitSlowDuration)
        {
            return 1f;
        }
        return 1f - hitSlowFactor + (Time.time - _lastHit)/hitSlowDuration*hitSlowFactor;
    }
}
