using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Scene objects")]
    public new Camera camera;
    public Transform lookAt;
    public Transform orbit;

    [Header("Config")]
    public float distance = 5f;
    public Vector2 xAngles = new Vector2(0f, 30f);
    public Vector2 yAngles = new Vector2(0f, 5f);
    public Vector2 timings = new Vector2(4f, 5f);
    public Vector2 angleJiggle = new Vector2(0.1f, 0.1f);

    void Update()
    {
        float t = Time.time*Mathf.PI*2f;
        float xAngle = xAngles.x + (Mathf.Sin(t/timings.x)+1f)/2f*(xAngles.y - xAngles.x);
        float yAngle = yAngles.x + (Mathf.Cos(t/timings.y)+1f)/2f*(yAngles.y - yAngles.x);
        Quaternion rotation = Quaternion.Euler(yAngle, xAngle, 0f);
        Vector3 offset = rotation*(Vector3.back*distance);
        Vector3 pos = orbit.position + offset;
        transform.position = pos;

        Vector3 jiggle = new Vector3(xAngle, yAngle, 0f)*angleJiggle;
        transform.LookAt(lookAt.position + jiggle);
    }
}
