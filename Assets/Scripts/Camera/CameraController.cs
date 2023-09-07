using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> gameCameras;

    private void Start()
    {
        gameCameras = new List<CinemachineVirtualCamera>();
    }
    
    
}
