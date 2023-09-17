using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehavior : MonoBehaviour
{
   private Rigidbody rb => GetComponent<Rigidbody>();
   public float flingSpeed = 10f;
   public float destroyHeight;

   private ParticleSystem hitFX;

   private void OnEnable()
   {
      PlayerHealth.OnEnemyKilled += GoFlying;
   }

   private void Start()
   {
      int random = Random.Range(0, GameParticles.Instance.hitParticles.Count);
      var hit = Instantiate(GameParticles.Instance.hitParticles[random], transform.GetChild(0));
      hitFX = hit.GetComponentInChildren<ParticleSystem>();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.tag == "Player")
      {
         GoFlying();
      }
   }
   public void GoFlying()
   {
      if (!PlayerHealth.Instance.CanKillEnemies)
      {
         return;
      }

      if (!hitFX.isPlaying)
      {
         hitFX.Play();
      }
      
      rb.isKinematic = false;
      rb.angularVelocity = new Vector3(10, 5, 0);
      rb.AddForce(Vector3.up * flingSpeed, ForceMode.Impulse);
      rb.AddForce(Vector3.forward * 10, ForceMode.Impulse);
   }

   private void Update()
   {
      if (transform.position.y >  destroyHeight)
      {
         Destroy(gameObject);
      }
   }
}
