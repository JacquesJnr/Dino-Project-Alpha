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

        if(activePhase.index + 1 == 1) Sfx.Instance.Play(Sfx.Instance.speedUp);
        if(activePhase.index + 1 == 2) Sfx.Instance.Play(Sfx.Instance.speedUp2);

        SetPhase(phases[activePhase.index+1]);
   }
   
   public void OnPhaseDecreased()
   {
      if (activePhase.index == 0)
      {
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
      
      // Get Battery
      BatteryPack.Instance.SetBatteryColor(phase.batteryColor);

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
      GameManager.Instance.gameSpeed = 0.34F;
   }

   public void Button_Phase2()
   {
      SetPhase(phases[1]);
      GameManager.Instance.gameSpeed = 0.64F;
   }

   public void Button_Phase3()
   {
      GameManager.Instance.gameSpeed = 1F;
      SetPhase(phases[2]);
   }
}
