using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PhaseCamera : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public float FOV
    {
        get { return cam.m_Lens.FieldOfView;}
        set { cam.m_Lens.FieldOfView = value; }
    }
}
