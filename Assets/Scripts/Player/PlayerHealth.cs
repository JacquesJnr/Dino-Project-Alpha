using System;
using System.Collections;

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public int maxHealth = 3;
    public float hitFreezeDuration = 0.2f;

    public int Health { get; set; }

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
                Health -= 1;
                StartCoroutine(FreezeGame(hitFreezeDuration));
                break;
        }
    }

    private IEnumerator FreezeGame(float time)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
    }
}
