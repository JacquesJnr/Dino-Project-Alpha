using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[System.Serializable]
public struct StateBody
{
    public Mode mode;
    public GameObject obj;
    public CinemachineVirtualCamera cam;
    public bool isStateActive() => StateMachine.Instance.GetState() == mode;
}

public class StateBodies : MonoBehaviour
{
    [SerializeField] private List<StateBody> bodies = new List<StateBody>();
    
    private void Start()
    {
        StateMachine.Instance.OnStateChanged += SetBody;
    }

    public void SetBody()
    {
        // Enable / Disable colliders and camera
        foreach (var b in bodies)
        {
            b.obj.SetActive(b.isStateActive());
            b.cam.enabled = b.isStateActive();
                
            if (b.cam.enabled)
            {
                
            }
        }
    }
}
