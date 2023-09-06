using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapParticles : MonoBehaviour
{
    public ParticleSystem poof;
    private void Awake()
    {
        StateMachine.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged()
    {
        poof.Play();
    }
}
