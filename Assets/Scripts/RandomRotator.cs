using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    public float speed = 30f;

    Vector3 rotation;

    void Start()
    {
        rotation = Random.onUnitSphere;
    }

    void Update()
    {
        transform.Rotate(rotation*Time.deltaTime*speed);
    }
}
