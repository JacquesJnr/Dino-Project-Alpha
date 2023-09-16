using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Bend config")]
    [Range(-0.015f, 0.015f)]
    public float bendRange;
    public float bendDuration = 1f;
    [NonSerialized] public float bend;
    
    [Header("CONTROLLERS")]
    public PlayerController runningControls;
    public Roller rollingControls;
    public Flight flyingControls;
    public bool grounded;
    
    public PlayerHealth playerHealth;

    [Header("VELOCITY PHASE")]
    [Range(0,1)]public float gameSpeed;
    public float acceleration;
    public AnimationCurve curve;
    
    [Header("STATE MACHINE")]
    public bool stateMachineEnabled;
    
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StateMachine.Instance.OnStateChanged += Swap;
        Swap();
    }

    public void Swap()
    {
       if(!stateMachineEnabled){return;}
       SetGameControls();
    }

    public void SetGameControls()
    {
        switch (StateMachine.Instance.GetState())
        {
            case Mode.Running:
                runningControls.enabled = true;
                rollingControls.enabled = false;
                flyingControls.enabled = false;
                
                grounded = !PlayerController.Instance.isJumping;
                break;
            case Mode.Rolling:
                rollingControls.enabled = true;
                runningControls.enabled = false;
                flyingControls.enabled = false;
                
                grounded = true;
                break;
            case Mode.Flying:
                runningControls.enabled = false;
                rollingControls.enabled = false;
                flyingControls.enabled = true;
                
                grounded = false;
                break;
        }
    }

    private void Update()
    {
        // World Bend
        float t = Time.time/bendDuration;
        bend = Mathf.Sin(t*2f*Mathf.PI)*bendRange;
        Shader.SetGlobalFloat("_Curvature", bend);
        
        // Increase Game Speed
        if(gameSpeed < 1)
        {
            gameSpeed += acceleration * curve.Evaluate(Time.deltaTime);
        }
        if (gameSpeed >= 1)
        {
            gameSpeed = 0;
        }
    }
}
