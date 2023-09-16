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
    public ParticleSystem speedLineFX;

    [Header("Ground FX Params")] 
    public float spawnInterval;
    private float timer = 0F;
    
    private Mode currentMode;

    private bool grounded => GameManager.Instance.grounded;

    public static GameParticles Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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
            case Mode.Rolling:
                jetPackFX.SetActive(false);
                break;
            case Mode.Running:
                jetPackFX.SetActive(false);
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

    public void OnPhaseChange(float dustSize, Vector3 lineSize)
    {
        var linesMain = speedLineFX.main;
        var dustMain = runDustFX.main;

        dustMain.startSize = dustSize;
        
        linesMain.startSizeX = lineSize.x;
        linesMain.startSizeY = lineSize.y;
        linesMain.startSizeZ = lineSize.z;
    }
}