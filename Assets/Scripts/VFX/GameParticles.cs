using System;
using UnityEngine;
public class GameParticles : MonoBehaviour
{
    private Transform playerVFX;

    [Header("Particles")]
    public GameObject stompFX;
    public ParticleSystem runDustFX;
    public GameObject jetPackFX;

    [Header("Ground FX Params")] 
    public float spawnInterval;
    private float timer = 0F;

    private bool grounded => GameManager.Instance.grounded;
    
    private void Start()
    {
        //TODO: Get Player Particle Transform
        playerVFX = GameObject.FindGameObjectWithTag("PlayerVFX").transform;
    }
    
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            RunVFX();
            timer = 0;
        }
        
        switch (StateMachine.Instance.GetState())
        {
            case Mode.Flying:
                // Jetpack FX   
                FlyVFX();
                break;
        }
    }

    public void RunVFX()
    {
        if (!grounded)
        {
            return;
        }
        
        InstanceAtGroundPosition(stompFX, playerVFX);
        runDustFX.gameObject.SetActive(true);
    }

    public void FlyVFX()
    {
        if(grounded){return;}
        jetPackFX.SetActive(true);
    }

    public void RollVFX()
    {
        // ...
    }
    
    public void InstanceAtGroundPosition(GameObject particle,Transform parent)
    {
        GameObject fx = Instantiate(particle, parent);
    }

}