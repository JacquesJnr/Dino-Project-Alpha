using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Roller : MonoBehaviour
{
   public Rigidbody rollBody;
   public float rollSpeed = 10F;
   public float maxSpeed = 5F;
   [Range(1, 10)] public float decelerationSpeed;
   private float startingDrag;

   private void Start()
   {
      startingDrag = rollBody.drag;
   }

   private void OnEnable()
   {
      AnimationManager.Instance.RollMode();
   }

   private void Update()
   {
      Vector3 moveVector = Vector3.zero;
      moveVector.x = Input.GetAxis("Horizontal");
      var force = moveVector * rollSpeed * Time.deltaTime;
      
      // Decelerate 
      rollBody.drag = moveVector.x != 0 ? startingDrag : decelerationSpeed;

      rollBody.AddForce(force, ForceMode.VelocityChange);
      // Clamp Horizontal speed
      rollBody.velocity = new Vector3(Mathf.Clamp(rollBody.velocity.x, -maxSpeed, maxSpeed), 0F, 0F);
   }

   private void OnCollisionEnter(Collision other)
   {
      // Cancel all forces for now so we can make out own bouncing logic
      rollBody.velocity = Vector3.zero;
   }
   
}
