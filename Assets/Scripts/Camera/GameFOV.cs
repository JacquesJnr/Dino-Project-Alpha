using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public struct HitCamera
{
   public GameObject obj;
   public CinemachineVirtualCamera cam;
   public GameObject gameCamBrain;
   public bool switchedOn => cam.enabled;
   public void SwitchToHitCam()
   {
      cam.enabled = true;
   }

   public void SwitchToGameCam()
   {
      cam.enabled = false;
   }
}

public class GameFOV : MonoBehaviour
{
   public HitCamera hitCamera;
   [Range(1,5)]public int delay = 1;
   public float hitFOV;

   private void OnEnable()
   {
      PlayerHealth.OnPlayerHit += PlayerHit;
   }
   
   private void PlayerHit()
   {
      // Set FOV When Hit
     PingPongFOV();
   }

   async void PingPongFOV()
   {
      // Switch to hit cam
      hitCamera.SwitchToHitCam();
      
      // Wait a lil while
      await Task.Delay(delay * 1000);
      
      // Return to game cam
      hitCamera.SwitchToGameCam();
   }

   private void Update()
   {
      hitCamera.cam.m_Follow = StateBodies.Instance.activeBody.cam.m_Follow;
      hitCamera.cam.m_LookAt = StateBodies.Instance.activeBody.cam.m_LookAt;
   }
}