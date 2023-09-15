using System;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class GameManager : MonoBehaviour
{
    [Header("Bend config")]
    [Range(-0.015f, 0.015f)]
    public float bendRange;
    public float bendDuration = 1f;

    public PlayerController runningControls;
    public Roller rollingControls;
    public Flight flyingControls;

    public PlayerHealth playerHealth;
    public Transform runningRoot, rollingRoot, flyingRoot;

    public static GameManager Instance;
    
    public bool grounded;

    [NonSerialized] public float bend;

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
        float t = Time.time/bendDuration;
        bend = Mathf.Sin(t*2f*Mathf.PI)*bendRange;
        Shader.SetGlobalFloat("_Curvature", bend);
    }
}
