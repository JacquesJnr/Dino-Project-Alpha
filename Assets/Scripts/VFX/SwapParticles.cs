using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapParticles : MonoBehaviour
{
    public ParticleSystem poof;
    [SerializeField]private Mode currentMode;
    private void Start()
    {
        StateMachine.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged()
    {
        if (currentMode != StateMachine.Instance.GetState())
        {
            poof.Play();
            currentMode = StateMachine.Instance.GetState();
        }
        
    }
}
