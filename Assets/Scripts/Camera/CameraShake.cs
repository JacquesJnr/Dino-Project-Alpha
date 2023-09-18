using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraShake : MonoBehaviour
{
    public float hitIntensity;
    public float velocityIntensity;
    public float timer = 1F;

   private CinemachineVirtualCamera activeCamera;

   private void Start()
   {
       PlayerHealth.OnPlayerHit += OnPlayerHit;
       GameManager.Instance.OnPhaseIncreased += VelocityUp;
       GameManager.Instance.OnPhaseIncreased += VelocityDown;
   }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerHit -= OnPlayerHit;
    }

    public void OnPlayerHit()
   {
      ShakeCamera(hitIntensity, 0.75F);
   }

   public void VelocityUp()
   {
       velocityIntensity += 0.25F;
   }
   
   private void VelocityDown()
   {
       velocityIntensity -= 0.25F;
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
       CinemachineBasicMultiChannelPerlin perlin =
           activeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
       
       // Hit
       if (timer > 0)
       {
           timer -= Time.deltaTime;
           if (timer <= 0)
           {
               perlin.m_AmplitudeGain = 0;
           }
       }

       perlin.m_AmplitudeGain = velocityIntensity;
   }
}
