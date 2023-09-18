using UnityEngine;

public class SimpleMover : MonoBehaviour
{
    public float relativeSpeed = 5f;
    public bool ignoreTileSpeed = false;

    void Update()
    {
        Vector3 pos = transform.position;
        float tileSpeed = ignoreTileSpeed ? 0f : GamePhases.Instance.activePhase.tileSpeed;
        pos.z -= PlayerController.Instance.DashSpeedMultipiplier*Time.deltaTime*(relativeSpeed + tileSpeed);
        transform.position = pos;
    }
}
