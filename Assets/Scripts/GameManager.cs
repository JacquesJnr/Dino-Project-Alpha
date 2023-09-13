using System;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class GameManager : MonoBehaviour
{
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
                //playerHealth.transform.SetParent(runningRoot, false);
                runningControls.enabled = true;
                rollingControls.enabled = false;
                flyingControls.enabled = false;
                grounded = !PlayerController.Instance.isJumping;
                break;
            case Mode.Rolling:
                //playerHealth.transform.SetParent(rollingRoot, false);
                rollingControls.enabled = true;
                runningControls.enabled = false;
                flyingControls.enabled = false;
                grounded = true;
                break;
            case Mode.Flying:
                //playerHealth.transform.SetParent(flyingRoot, false);
                runningControls.enabled = false;
                rollingControls.enabled = false;
                flyingControls.enabled = true;
                grounded = false;
                break;
        }
    }
}
