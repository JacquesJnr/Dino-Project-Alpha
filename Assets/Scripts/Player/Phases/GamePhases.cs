using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePhases : MonoBehaviour
{
 
   [SerializeField] private List<Phase> phases;
   public Phase activePhase;
   public Sprite hitPortrait;
   public Sprite deadPortrait;

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

      StartCoroutine(HitPortrait());
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

   public IEnumerator HitPortrait()
   {
      
      SetPhase(phases[activePhase.index-1]);
      PhaseUI.Instance.SetPlayerPortrait(hitPortrait);
      PhaseUI.Instance.HitAlpha();
      yield return new WaitForSeconds(GameManager.Instance.delay);
      PhaseUI.Instance.SetPlayerPortrait(activePhase.portrait);
   }

   public void Button_Phase1()
   {
      SetPhase(phases[0]);
   }

   public void Button_Phase2()
   {
      SetPhase(phases[1]);
   }

   public void Button_Phase3()
   {
      SetPhase(phases[2]);
   }
}
