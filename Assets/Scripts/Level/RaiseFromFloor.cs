using System.Collections;

using UnityEngine;

public class RaiseFromFloor : MonoBehaviour
{
    public float raiseDuration = 1f;
    public float raiseStartY;
    public float raiseTargetY;

    private void Start()
    {
        Vector3 pos = transform.position;
        pos.y = raiseStartY;
        StartCoroutine(RaiseFromFloorRoutine());
    }

    IEnumerator RaiseFromFloorRoutine()
    {
        float start = Time.time;

        Vector3 pos = transform.position;
        pos.y = raiseStartY;
        transform.position = pos;
        while(Time.time < start + raiseDuration)
        {
            float t = (Time.time - start)/raiseDuration;
            pos = transform.position;
            pos.y = Mathf.Lerp(raiseStartY, raiseTargetY, t);
            transform.position = pos;
            yield return null;
        }

        pos.y = raiseTargetY;
        transform.position = pos;
        yield return null;
    }
}
