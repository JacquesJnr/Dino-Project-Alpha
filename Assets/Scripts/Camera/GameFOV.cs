using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class GameFOV : MonoBehaviour
{
   [SerializeField] private StateBody activeCamera;
   [Range(1,5)]public int slowTime = 1;
   

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
      activeCamera.hitCam.enabled = true;
      await Task.Delay(slowTime * 1000);
      activeCamera.hitCam.enabled = false;
   }

   private void Update()
   {
      // Get Active Game Camera
      activeCamera = StateBodies.Instance.activeBody;
   }
   
   
}