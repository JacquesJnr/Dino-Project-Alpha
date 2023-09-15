using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseParticles : MonoBehaviour
{
   [SerializeField] private ParticleSystem runDust;
   [SerializeField] private ParticleSystem speedLines;
   public float runDustSize;
   public Vector3 speedLinesSize;

   private void Update()
   {
      runDust.startSize = runDustSize;

      var main = speedLines.main;
      main.startSizeX = speedLinesSize.x;
      main.startSizeY = speedLinesSize.y;
      main.startSizeZ = speedLinesSize.z;
   }
}
