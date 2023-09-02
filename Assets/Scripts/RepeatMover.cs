using UnityEngine;

public class RepeatMover : MonoBehaviour
{
    public float start = 80f;
    public float distance = 100f;
    public float duration = 5f;

    void Update()
    {
        var pos = transform.position;
        pos.z = start - Time.time/duration % 1f * distance;
        transform.position = pos;
    }
}
