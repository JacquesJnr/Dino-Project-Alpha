using UnityEngine;

public class SimpleMover : MonoBehaviour
{
    public float relativeSpeed = 5f;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z -= Time.deltaTime*(relativeSpeed + PlayerController.Instance.Speed);
        transform.position = pos;
    }
}
