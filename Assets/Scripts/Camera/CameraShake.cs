using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeIntensity;
    public float timer = 1F;

   private CinemachineVirtualCamera activeCamera;

   private void Start()
   {
       PlayerHealth.OnPlayerHit += OnPlayerHit;
   }

   public void OnPlayerHit()
   {
      ShakeCamera(shakeIntensity, 0.75F);
   }

   public void ShakeCamera(float intensity, float time)
   {
       CinemachineBasicMultiChannelPerlin perlin =
           activeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

       perlin.m_AmplitudeGain = intensity;
       timer = time;
   }

   private void Update()
   {
       activeCamera = StateBodies.Instance.activeBody.cam;
       
       if (timer > 0)
       {
           timer -= Time.deltaTime;
           if (timer <= 0)
           {
               CinemachineBasicMultiChannelPerlin perlin =
                   activeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

               perlin.m_AmplitudeGain = 0;
           }
       }
   }
}
