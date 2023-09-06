using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyUp : MonoBehaviour
{
    private float groundHeight;
    public float flyHeight = 10f;
    public float duration = 1f;
    private float lerp = 0;
   private void Awake()
   {
       groundHeight = transform.position.y;
       StateMachine.OnStateChanged += Fly;
   }
   private IEnumerator LerpY(float targetHeight)
   {
       float time = 0;
       float startValue = transform.position.y;
       float endValue = targetHeight == flyHeight ? flyHeight : groundHeight;
       while (time < duration)
       {
           lerp = Mathf.Lerp(startValue, endValue, time / duration);
           time += Time.deltaTime;
           
           var newPos = new Vector3(transform.position.x, lerp, transform.position.z);
           transform.position = newPos;
           
           yield return null;
       }
       lerp = endValue;
   }
   private void Fly()
   {
       if (StateMachine.Instance.GetState() != Mode.Flying)
       {
           StartCoroutine(LerpY(groundHeight));
           return;
       }
       StartCoroutine(LerpY(flyHeight));
   }
}
