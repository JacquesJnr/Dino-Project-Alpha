using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Phase
{ 
   [Header("Phase Data")]
   public string name;
   public int index;
   public float FOV;

   public float tileSpeed;
   public float obstacleDistance;

   // UI Bar Value - either 0.33, 0.66 or 1
   [Range(0,1)]public float ui_velocity;
   
   [Header("Player Portrait")]
   // Player UI Portrait
   public Sprite portrait;
   
   [Header("Particles")]
   // Run Dust Size
   [Range(0.24F, 1F)]public float dustSize;

   // Speed Lines
   public Vector3 lineSize = new Vector3(0.16F, 1.8F, 0.5F);
   
   [Header("Battery")]
   public Color batteryColor;
}