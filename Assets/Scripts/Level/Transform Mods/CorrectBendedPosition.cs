using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

/// <summary>
/// To set light sources at the correct position
/// </summary>
public class CorrectBendedPosition : MonoBehaviour
{
    public float distanceToCamera;
    public float originalXPos;
    public float offsetX;
    public float currentXPos;

    private Camera mainCamera;

    void Start()
    {
        originalXPos = transform.position.x;
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 position = transform.position;
        distanceToCamera = position.z - mainCamera.transform.position.z;
        offsetX = Mathf.Pow(distanceToCamera, 2)*GameManager.Instance.bend;
        position.x = originalXPos + offsetX;
        currentXPos = position.x;
        transform.position = position;
    }
}
