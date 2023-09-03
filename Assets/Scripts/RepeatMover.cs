using UnityEngine;

public class RepeatMover : MonoBehaviour
{
    public float start = 80f;
    public float distance = 100f;
    public float duration = 5f;
    public float speed = 5f;

    float Speed => distance/duration*PlayerController.Instance.speed;

    private void Start()
    {
        Vector3 pos = transform.position;
        pos.z = start;
        transform.position = pos;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z -= Speed*Time.deltaTime;

        pos.z = pos.z % (distance - start);
        transform.position = pos;
    }
}
