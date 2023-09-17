using UnityEngine;

public class SimpleMover : MonoBehaviour
{
    public float relativeSpeed = 5f;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z -= PlayerController.Instance.DashSpeedMultipiplier*Time.deltaTime*(relativeSpeed + GamePhases.Instance.activePhase.tileSpeed);
        transform.position = pos;
    }
}
