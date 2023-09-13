using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunParticles : MonoBehaviour
{
    private Transform particleParent;
    
    [Header("Particles")]
    public GameObject stompFX;
    public ParticleSystem runDustFX;
    public GameObject jetPackFX;

    [Header("Spawner")]
    public float spawnInterval = 1F;
    public float timer = 0F;

    private void Start()
    {
        particleParent = transform;
    }

    private void Update()
    {
        timer += Time.deltaTime;
            
        if (timer >= spawnInterval)
        {
            timer = 0;
            if (GameManager.Instance.grounded)
            {
                // Ground Stomp
                if (StateMachine.Instance.GetState() == Mode.Running)
                { 
                    InstanceAtGroundPosition(stompFX, particleParent);
                } 
                if (StateMachine.Instance.GetState() == Mode.Rolling)
                { 
            
                } 
            }
        }
        
                    
        // Dust Trails
        runDustFX.gameObject.SetActive(GameManager.Instance.grounded);
        // Jetpack FX
        jetPackFX.SetActive(!GameManager.Instance.grounded);
    }

    public void InstanceAtGroundPosition(GameObject particle,Transform parent)
    {
        GameObject fx = Instantiate(particle, transform);
    }
}
