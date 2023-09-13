using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{ 
    public GameObject flyBody;
    
    public float speed = 10F;
    public float acelleration = 2.5F;
    private float activeHorizontalSpeed, activeVerticalSpeed;

    public float flyHeight;
    private float groundHeight;
    
    public Vector3 bound = new Vector3();

    private void OnEnable()
    {
        StartCoroutine(AscendDecend(flyHeight));
        AnimationManager.Instance.FlyMode();
    }

    private void Start()
    {
        groundHeight = flyBody.transform.position.y;
    }

    private void Update()
    {
        activeHorizontalSpeed = Mathf.Lerp
        (
            activeHorizontalSpeed, 
            Input.GetAxisRaw("Horizontal") * speed, 
            acelleration * Time.deltaTime
        );
        
        activeVerticalSpeed = Mathf.Lerp
        (
            activeVerticalSpeed, 
            Input.GetAxisRaw("Vertical") * speed,   
            acelleration * Time.deltaTime
        );
        
        // Move
        flyBody.transform.position += 
        (
            (flyBody.transform.right * activeHorizontalSpeed * Time.deltaTime) + 
            (flyBody.transform.up * activeVerticalSpeed * Time.deltaTime)
        );
        
        // Bounds Check
        bool withinX = Mathf.Abs(flyBody.transform.position.x) < bound.x / 2;
        bool withinY = Mathf.Abs(flyBody.transform.position.y ) - flyHeight < bound.y / 2;
        bool withinBounds = withinX && withinY;
        
        if (flyBody.transform.position.x < -bound.x / 2)
        {
            activeHorizontalSpeed = Mathf.Clamp(activeHorizontalSpeed, 0, float.MaxValue);
        } 
        if (flyBody.transform.position.x > bound.x / 2)
        {
            activeHorizontalSpeed = Mathf.Clamp(activeHorizontalSpeed, float.MinValue, 0);
        }
        if (flyBody.transform.position.y - flyHeight > bound.y / 2)
        {
            activeVerticalSpeed = Mathf.Clamp(activeVerticalSpeed, float.MinValue, 0);
        } 
        if (flyBody.transform.position.y- flyHeight < -bound.y / 2)
        {
            activeVerticalSpeed = Mathf.Clamp(activeVerticalSpeed, 0, float.MaxValue);
        }
    }
    
    // Fly up / Come down
    public float ascendSpeed = 1f;
    private float lerp = 0;
    
    private IEnumerator AscendDecend(float targetHeight)
    {
        float time = 0;
        float startValue = flyBody.transform.position.y;
        float endValue = targetHeight == flyHeight ? flyHeight : groundHeight;
        while (time < ascendSpeed)
        {
            lerp = Mathf.Lerp(startValue, endValue, time / ascendSpeed);
            time += Time.deltaTime;
           
            var newPos = new Vector3(flyBody.transform.position.x, lerp, flyBody.transform.position.z);
            flyBody.transform.position = newPos;
           
            yield return null;
        }
        lerp = endValue;
        
    }
    
    // Draw bounds
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector3(0, flyHeight, 0), bound);
    }

    private void OnDisable()
    {
        StartCoroutine(AscendDecend(groundHeight));
    }
}
