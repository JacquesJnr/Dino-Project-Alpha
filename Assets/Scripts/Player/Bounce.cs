using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public static event Action OnBounce;

    public void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Wall")
        {
            Debug.Log("Bouncing");
            OnBounce?.Invoke();
        }
    }
}
