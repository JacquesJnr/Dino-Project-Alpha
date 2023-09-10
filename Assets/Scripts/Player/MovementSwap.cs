using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : Integrate All 3 Controllers into one more elegantly so we don'y have to do this, maybe

public class MovementSwap : MonoBehaviour
{
    public PlayerController runningControls;
    public Roller rollingControls;
    public Flight flyingControls;

    private void Start()
    {
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
                break;
            case Mode.Rolling:
                rollingControls.enabled = true;
                runningControls.enabled = false;
                flyingControls.enabled = false;
                break;
            case Mode.Flying:
                runningControls.enabled = false;
                rollingControls.enabled = false;
                flyingControls.enabled = true;
                break;
        }
    }
}
