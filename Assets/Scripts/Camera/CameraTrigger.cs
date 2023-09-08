using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraTrigger : MonoBehaviour
{
    public UnityEvent cameraBehavior;

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            var allTriggers = new List<GameObject>();
            foreach (var go in GameObject.FindGameObjectsWithTag("CameraTigger"))
            {
                allTriggers.Add(go);
                go.SetActive(false);
            }
            
            cameraBehavior.Invoke();

            if (transform.name.Contains("State"))
            {
                SwapStates(transform);
            }
        }
    }
    public void SwapStates(Transform obj)
    {
        string[] directions = name.Split('-');
        Mode newState = Mode.Running;
        
        switch (directions[1])
        {
            case "Running":
                newState = Mode.Running;
                break;
            case "Rolling":
                newState = Mode.Rolling;
                break;
            case "Flying":
                newState = Mode.Flying;
                break;
        }
        
        StateMachine.Instance.SetState(newState);
    }
}
