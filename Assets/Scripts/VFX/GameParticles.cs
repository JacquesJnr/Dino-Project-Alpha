using System;
using UnityEngine;
public class GameParticles : MonoBehaviour
{
    private Transform playerVFX;

    [Header("Particles")]
    public GameObject stompFX;
    public ParticleSystem runDustFX;
    public GameObject jetPackFX;
    public ParticleSystem swapFX;

    [Header("Ground FX Params")] 
    public float spawnInterval;
    private float timer = 0F;
    
    private Mode currentMode;

    private bool grounded => GameManager.Instance.grounded;
    
    private void Start()
    {
        //TODO: Get Player Particle Transform
        playerVFX = GameObject.FindGameObjectWithTag("PlayerVFX").transform;
        
        StateMachine.Instance.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged()
    {
        if (currentMode != StateMachine.Instance.GetState())
        {
            swapFX.Play();
            currentMode = StateMachine.Instance.GetState();
        }
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