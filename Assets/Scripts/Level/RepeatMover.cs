using UnityEngine;

public class RepeatMover : MonoBehaviour
{
    public float relativeSpeed = 5f;
    public float transitPosition = -25f;
    public float startPosition = 80f;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z -= PlayerController.Instance.DashSpeedMultipiplier*Time.deltaTime*(relativeSpeed + GamePhases.Instance.activePhase.tileSpeed);
        if(pos.z < transitPosition)
        {
            pos.z = startPosition;
        }
        transform.position = pos;
    }
}
