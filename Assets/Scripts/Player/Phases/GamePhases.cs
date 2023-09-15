using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GamePhases : MonoBehaviour
{
   //TODO: Copy / Paste into Game Manager when ready & delete GamePhases.cs
   
   [SerializeField] private List<Phase> gamePhases;
   public Phase activePhase;
   
   [Range(0,1)]public float gameSpeed;
   public float acceleration;
   public AnimationCurve curve;

   public static GamePhases Instance;

   private PhaseCamera phaseCamera;
   private PhaseUI phaseUI;
   private PhaseParticles phaseParticles;

   private void Start()
   {
      Instance = this;
      activePhase = gamePhases[0];

      phaseCamera = FindObjectOfType<PhaseCamera>();
      phaseUI = FindObjectOfType<PhaseUI>();
      phaseParticles = FindObjectOfType<PhaseParticles>();
      
      SetPhase(activePhase);
   }

   public void IncreasePhase(int index)
   {
      if (index == 2)
      {
         Debug.Log("Sanity Check ");
         return;
      }

      SetPhase(gamePhases[index+1]);
   }

   public void DeacreasePhase(int index)
   {
      if (index < 0)
      {
         return;
      }
      
      SetPhase(gamePhases[index-1]);
   }

   public void SetPhase(Phase phase)
   {
      // Get Camera
      phaseCamera.FOV = phase.FOV;

      // Get UI
      phaseUI.velocityBar = phase.ui_velocity;
      phaseUI.playerPortrait = phase.portrait;

      // Get Particles
      phaseParticles.runDustSize = phase.dustSize;
      phaseParticles.speedLinesSize = phase.lineSize;

      activePhase = phase;
   }
   
   public void GetHit()
   {
      // Swap Portrait On Hit
      DeacreasePhase(activePhase.index);
      gameSpeed = 0;
   }

   private void Update()
   {
      // Increase Speed Gradually
      if(gameSpeed < 1)
      {
         gameSpeed += acceleration * curve.Evaluate(Time.deltaTime);
      }
      if (gameSpeed >= 1)
      {
         IncreasePhase(activePhase.index);
         gameSpeed = 0;
      }
   }
}
