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
    public PlayerHealth health;
    public bool isStateActive() => StateMachine.Instance.GetState() == mode;
    public Vector3 GetPos()
    {
        return obj.transform.localPosition;
    }
}

public class StateBodies : MonoBehaviour
{
    public StateBody activeBody;
    
    [SerializeField] private List<StateBody> bodies = new List<StateBody>();
    [SerializeField] private Animator anim;
    
    private Vector3 pos;

    public static StateBodies Instance;

    private void Awake()
    {
        activeBody = bodies[0];
    }

    private void Start()
    {
        Instance = this;
        StateMachine.Instance.OnStateChanged += SetBody;
    }

    public void SetBody()
    {
        // Enable / Disable colliders and camera
        foreach (var b in bodies)
        {
            b.obj.SetActive(b.isStateActive());
            b.cam.enabled = b.isStateActive();

            if (b.isStateActive())
            {
                activeBody = b;
            }
        }
    }

    private void Update()
    {
        CenterAnimOnBody();
    }
    
    public void CenterAnimOnBody()
    {
        anim.transform.localPosition = activeBody.GetPos();
    }
}
