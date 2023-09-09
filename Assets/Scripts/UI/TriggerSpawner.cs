using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour
{
   public GameObject runTrigger;
   public GameObject rollTrigger;
   public GameObject flyTrigger;

   private List<GameObject> garbage = new List<GameObject>();

   public void InstanceRun()
   {
      GameObject run = Instantiate(runTrigger);
      garbage.Add(run);
   }
   
   public void InstanceRoll()
   {
      GameObject roll = Instantiate(rollTrigger);
      garbage.Add(roll);
   }
   
   public void InstanceFly()
   {
      GameObject fly = Instantiate(flyTrigger);
      garbage.Add(fly);
   }

   private void Update()
   {
      foreach (var go in garbage)
      {
         if (go.transform.position.z <= -20)
         {
            go.SetActive(false);
         }
      }
   }
}
