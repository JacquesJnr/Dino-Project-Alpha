using UnityEngine;

public class RepeatMover : MonoBehaviour
{
    public float speed = 5f;
    public float transitPosition = -25f;
    public float startPosition = 80f;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z -= Time.deltaTime*speed;
        if(pos.z < transitPosition)
        {
            pos.z = startPosition;
        }
        transform.position = pos;
    }
}
