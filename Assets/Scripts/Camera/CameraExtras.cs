using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script does specific manipulations on a CineMachine parameters e.g transitions, body & aim
/// it is NOT for switching Cameras.
/// </summary>

public class CameraExtras : MonoBehaviour
{
    public ICinemachineCamera live => CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;
    public CinemachineBrain brain => GetComponent<CinemachineBrain>();
    
    public GameCamera Run;

    public static CameraExtras Instance;

    private void Start()
    {
        Instance = this;
        
        Run.tranposer = Run.cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        Run.body = Run.cam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }

    private void Update()
    {
        switch (StateMachine.Instance.GetState())
        {
            case Mode.Running:
                SetCameraDeadzone();
                break;
            default:
                break;
        }
    }

    private void SetCameraDeadzone()
    {
        bool inMiddle = PlayerController.Instance.indexedPosition == 0;

        // Center the camera when the player is in the middle lane
        if (inMiddle)
        {
            Run.deadZoneX = 0;
        }
        else // Otherwise let the player move off-center
        {
            Run.deadZoneX = 0.32F;
        }
    }
}