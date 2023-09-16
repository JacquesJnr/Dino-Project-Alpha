using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GamePhases : MonoBehaviour
{
 
   [FormerlySerializedAs("gamePhases")] [SerializeField] private List<Phase> phases;
   public Phase activePhase;

   public static GamePhases Instance;

   private void Awake()
   {
      Instance = this;
   }

   private void Start()
   {
      activePhase = phases[0];
      SetPhase(activePhase);

      GameManager.Instance.OnPhaseIncreased += OnPhaseIncrease;
      GameManager.Instance.OnPhaseDecreased += OnPhaseDecreased;
   }

   public void OnPhaseIncrease()
   {
      if (activePhase.index == phases.Count - 1)
      {
         Debug.Log("Max Phase Reached");
         return;
      }
      
      SetPhase(phases[activePhase.index+1]);
   }
   
   public void OnPhaseDecreased()
   {
      if (activePhase.index == 0)
      {
         Debug.Log("Min Phase Reached");
         return;
      }
      
      SetPhase(phases[activePhase.index-1]);
   }

   public void SetPhase(Phase phase)
   {
      // Get Camera
      GameFOV.Instance.SetFOV(phase.FOV);

      // Get Particles
      GameParticles.Instance.OnPhaseChange(phase.dustSize, phase.lineSize);
      
      // Get UI
      PhaseUI.Instance.SetPlayerPortrait(phase.portrait);

      activePhase = phase;
   }
   
}
