using System.Collections;

using UnityEngine;

public class RepeatMover : MonoBehaviour
{
    public float relativeSpeed = 5f;
    public float transitPosition = -25f;
    public float startPosition = 80f;

    [Header("Raising from floor")]
    public bool raisesFromFloor;
    public float raiseStartY;
    public float raiseTargetY;

    private void Start()
    {
        if(raisesFromFloor)
        {
            StartCoroutine(RaiseFromFloorRoutine());
        }
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.z -= Time.deltaTime*(relativeSpeed + PlayerController.Instance.speed);
        if(pos.z < transitPosition)
        {
            pos.z = startPosition;
            if(raisesFromFloor)
            {
                StartCoroutine(RaiseFromFloorRoutine());
            }
        }
        transform.position = pos;
    }

    IEnumerator RaiseFromFloorRoutine()
    {
        float start = Time.time;
        const float DURATION = 1f;

        Vector3 pos = transform.position;
        pos.y = raiseStartY;
        transform.position = pos;
        while(Time.time < start + DURATION)
        {
            float t = (Time.time - start)/DURATION;
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
