using System;

using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public int maxHealth = 3;

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
                break;
            default:
                throw new Exception($"Missing case: {other.tag}");
        }
    }
}
