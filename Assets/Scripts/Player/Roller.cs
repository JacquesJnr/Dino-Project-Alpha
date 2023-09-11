using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Roller : MonoBehaviour
{
   public Rigidbody rollBody;
   
   [Header("Rolling")]
   public float rollSpeed = 10F;
   public float maxSpeed = 5F;
   [Range(1, 10)] public float decelerationSpeed;

   [Header("Bounce")] 
   public float bouncingForce;
   public bool bouncing;
   
   private float startingDrag;

   private void Start()
   {
      startingDrag = rollBody.drag;
   }

   private void OnEnable()
   {
      AnimationManager.Instance.RollMode(); 
      Bounce.OnBounce += OnBounce;
   }

   private void Update()
   {
      Vector3 moveVector = Vector3.zero;
      moveVector.x = Input.GetAxis("Horizontal");
      var force = moveVector * rollSpeed * Time.deltaTime;
      
      // Decelerate 
      if (!bouncing)
      {
         rollBody.drag = moveVector.x != 0 ? startingDrag : decelerationSpeed;
      }
      else
      {
         rollBody.drag = startingDrag;
      }

      rollBody.AddForce(force, ForceMode.VelocityChange);
      // Clamp Horizontal speed
      rollBody.velocity = new Vector3(Mathf.Clamp(rollBody.velocity.x, -maxSpeed, maxSpeed), 0F, 0F);
   }
   private void OnBounce()
   {
      bool leftWall = rollBody.transform.position.x < 0;
      bool rightWall = rollBody.transform.position.x > 0;

      if (leftWall)
      {
         rollBody.AddForce(Vector3.right * bouncingForce,ForceMode.Impulse);
         bouncing = true;
      }
      if (rightWall)
      {
         rollBody.AddForce(Vector3.left * bouncingForce, ForceMode.Impulse);
         bouncing = true;  
      }

      bouncing = false;
   }

   private void OnDisable()
   {
      Bounce.OnBounce -= OnBounce;
   }
}
