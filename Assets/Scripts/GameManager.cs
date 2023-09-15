using System;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class GameManager : MonoBehaviour
{
    [Range(-0.015f, 0.015f)]
    public float bend;
    public PlayerController runningControls;
    public Roller rollingControls;
    public Flight flyingControls;

    public PlayerHealth playerHealth;
    public Transform runningRoot, rollingRoot, flyingRoot;

    public static GameManager Instance;
    
    public bool grounded;

    private void Start()
    {
        Instance = this;
        StateMachine.Instance.OnStateChanged += Swap;
        Swap();
    }

    public void Swap()
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
        Shader.SetGlobalFloat("_Curvature", bend);
    }
}
