using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GamePhases : MonoBehaviour
{
 
   [SerializeField] private List<Phase> gamePhases;
   public Phase activePhase;

   public static GamePhases Instance;
   
   private PhaseUI phaseUI;
   private PhaseParticles phaseParticles;

   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      activePhase = gamePhases[0];
      
      phaseUI = FindObjectOfType<PhaseUI>();
      phaseParticles = FindObjectOfType<PhaseParticles>();
      
      SetPhase(activePhase);
   }

   public void SetPhase(Phase phase)
   {
      // Get Camera
      GameFOV.Instance.SetFOV(phase.FOV);

      // // Get UI
      // phaseUI.velocityBar = phase.ui_velocity;
      // phaseUI.playerPortrait = phase.portrait;
      //
      // // Get Particles
      // phaseParticles.runDustSize = phase.dustSize;
      // phaseParticles.speedLinesSize = phase.lineSize;

      activePhase = phase;
   }

   public void Phase2()
   {
      SetPhase(gamePhases[1]);
   }

   // public void IncreasePhase(int index)
   // {
   //    if (index == 2)
   //    {
   //       Debug.Log("Sanity Check ");
   //       return;
   //    }
   //
   //    SetPhase(gamePhases[index+1]);
   // }
   //
   // public void DeacreasePhase(int index)
   // {
   //    if (index < 0)
   //    {
   //       return;
   //    }
   //    
   //    SetPhase(gamePhases[index-1]);
   // }
   // public void GetHit()
   // {
   //    // Swap Portrait On Hit
   //    DeacreasePhase(activePhase.index);
   // }
}
