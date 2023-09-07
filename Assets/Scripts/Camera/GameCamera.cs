using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class GameCamera
{
    public CinemachineVirtualCamera cam;
    [HideInInspector]public CinemachineFramingTransposer tranposer;
    [HideInInspector]public Cinemachine3rdPersonFollow body;
    
    public float deadZoneX
    {
        get { return tranposer.m_DeadZoneWidth; }
        set { tranposer.m_DeadZoneWidth = value; }
    }
    
    public float FOV
    {
        get { return cam.m_Lens.FieldOfView; } 
        set { tranposer.m_DeadZoneWidth = value;}
    }

    public Vector3 objectOffset
    {
        get { return tranposer.m_TrackedObjectOffset; }
    }

    public Vector3 thirdPersonDamping
    {
        get { return body.Damping; }
        set { body.Damping = value; }
    }
}