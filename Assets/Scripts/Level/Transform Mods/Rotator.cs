using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 speed = Vector3.forward*50f;
    public bool randomStartAngle;

    private void Start()
    {
        if(randomStartAngle)
        {
            transform.localRotation *= Quaternion.Euler(speed*Random.value*360f);
        }
    }

    void Update()
    {
        transform.localRotation *= Quaternion.Euler(speed*Time.deltaTime);
    }
}
