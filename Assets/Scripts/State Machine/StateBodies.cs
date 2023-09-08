using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StateBody
{
    public Mode mode;
    public GameObject obj;
    public bool isStateActive() => StateMachine.Instance.GetState() == mode;
}

public class StateBodies : MonoBehaviour
{
    [SerializeField] private List<StateBody> bodies = new List<StateBody>();

    
    private void Awake()
    {
        StateMachine.OnStateChanged += SetBody;
    }

    public void SetBody()
    {
        foreach (var b in bodies)
        {
            b.obj.SetActive(b.isStateActive());
        }
    }
}
