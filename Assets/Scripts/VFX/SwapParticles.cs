using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapParticles : MonoBehaviour
{
    public ParticleSystem poof;
    [SerializeField]private Mode currentMode;
    private void Awake()
    {
        StateMachine.OnStateChanged += OnStateChanged;
        currentMode = StateMachine.Instance.GetState();
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
