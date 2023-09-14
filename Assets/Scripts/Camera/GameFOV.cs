using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class GameFOV : MonoBehaviour
{
   public CinemachineVirtualCamera activeCamera;
   public AnimationCurve slowFactor;
   public AnimationCurve rampUp;

   public float decreaseSpeed = 1F;
   public float hitTarget = 60F;

   private void OnEnable()
   {
      PlayerHealth.OnPlayerHit += PlayerHit;
   }

   private void PlayerHit()
   {
      // Set FOV When Hit
      DecreaseFOVSmoothly();
   }

   private IEnumerator DecreaseFOVSmoothly()
   {
      float start = Time.time;
      
      while (Time.time < activeCamera.m_Lens.FieldOfView + decreaseSpeed)
      {
         float t = slowFactor.Evaluate((Time.time - start)/decreaseSpeed);
         LensSettings lens = activeCamera.m_Lens;
         lens.FieldOfView = hitTarget * t;
         yield return null;
      }

      activeCamera.m_Lens.FieldOfView = hitTarget;
   }

   private void Update()
   {
      // Get Active Game Camera
      activeCamera = StateBodies.Instance.activeBody.cam;
   }
   
   
}