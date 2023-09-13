using UnityEngine;

public class RepeatMover : MonoBehaviour
{
    public float relativeSpeed = 5f;
    public float transitPosition = -25f;
    public float startPosition = 80f;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z -= Time.deltaTime*(relativeSpeed + PlayerController.Instance.Speed);
        if(pos.z < transitPosition)
        {
            pos.z = startPosition;
        }
        transform.position = pos;
    }
}
