using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameFOV : MonoBehaviour
{
    public CinemachineVirtualCamera activeGameCamera;
    public float fovDuration;

    public static GameFOV Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public void SetFOV(float FOV)
    {
        StartCoroutine(FOVChange(FOV));
    }
    
    private IEnumerator FOVChange(float targetFOV)
    {
        float time = Time.time;
        float start = activeGameCamera.m_Lens.FieldOfView;
        while (time < fovDuration)
        {
            // Increse FOV
            var lerp = Mathf.Lerp(start, targetFOV, time / fovDuration);
            activeGameCamera.m_Lens.FieldOfView = lerp;
            
            time += Time.deltaTime;
            yield return null;
        }
        
        activeGameCamera.m_Lens.FieldOfView = targetFOV;
    }

    private void Update()
    {
        activeGameCamera = StateBodies.Instance.activeBody.cam;
    }
}