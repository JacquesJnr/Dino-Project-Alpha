using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [SerializeField] private float hitFreezeDuration = 0.2f;
    [SerializeField] private float hitSlowFactor = 0.5f;
    [SerializeField] public float hitSlowDuration = 0.5f;
    public int Health { get; private set; }

    private float _lastHit = -999;
    
    public bool CanKillEnemies;
    public bool FreeHit;
    public int freeHitCount;
    public static event Action OnPlayerHit;
    public static event Action OnEnemyKilled;

    public GameOverMenu gameOver;

    void Start()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Obstacle":
                
                if (!FreeHit)
                {
                    Hit(other.name);
                }
                else
                {
                    PhaseInteractions();
                }

                break;
            case "Enemy":
                
                if (!CanKillEnemies)
                {
                    Hit(other.name);
                }

                // if (!FreeHit)
                // {
                //     Hit(other.name);
                // }

                PhaseInteractions();
                break;
        }
    }
    
    private IEnumerator FreezeGame(float time)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(time);
        if(!GameManager.Instance.gameOverMenu.isActiveAndEnabled)
        {
            // don't unpause time if the pausemenu is opened
            Time.timeScale = 1f;
        }
    }

    public float GetSlowFactor()
    {
        if(Time.time > _lastHit + hitSlowDuration)
        {
            return 1f;
        }
        return 1f - hitSlowFactor + (Time.time - _lastHit)/hitSlowDuration*hitSlowFactor;
    }

    public void Hit(string name)
    {
        _lastHit = Time.time;
        StartCoroutine(FreezeGame(hitFreezeDuration));
        OnPlayerHit?.Invoke();
        gameOver.SetKillInfo(name);
    }

    public void PhaseInteractions()
    {
        if (FreeHit)
        {
            --freeHitCount;
        }
        
        if (CanKillEnemies)
        {
            Sfx.Instance.Play(Sfx.Instance.playerCollision);
            OnEnemyKilled?.Invoke();
        }
    }

    public void Button_SimulateHit()
    {
        OnPlayerHit?.Invoke();
    }

    private void Update()
    {
        CanKillEnemies = GameManager.Instance.gameSpeed > 0.33F;
        FreeHit = GameManager.Instance.gameSpeed > 0.66F && freeHitCount > 0;
    }
}
