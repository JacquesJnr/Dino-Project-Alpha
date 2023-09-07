using System;

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;

    public static PlayerHealth Instance;

    void Start()
    {
        Instance = this;
        health = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Obstacle":
                health -= 1;
                break;
            default:
                throw new Exception($"Missing case: {other.tag}");
        }
    }
}
