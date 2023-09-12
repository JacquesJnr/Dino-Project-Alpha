using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    [Header("Config")]
    public float moveDuration;
    public float spinDuration;
    public float startAngle;
    public float targetAngle;
    public float sequenceDelay;
    public float moveUpDelay;

    [Header("References")]
    public Transform root;
    public Transform blade;

    void Start()
    {
        StartCoroutine(BladeRoutine());
    }

    private IEnumerator BladeRoutine()
    {
        float start, angle, t;
        while(true)
        {
            start = Time.time;
            while(Time.time < start + moveDuration)
            {
                t = (Time.time - start)/moveDuration;
                angle = Mathf.Lerp(startAngle, targetAngle, t);
                root.localEulerAngles = new Vector3(0f, 0f, angle);
                yield return null;
            }

            start = Time.time;
            while(Time.time < start + spinDuration)
            {
                t = (Time.time - start)/spinDuration;
                angle = t*360f;
                blade.localEulerAngles = new Vector3(0f, 0f, angle);
                yield return null;
            }
            yield return new WaitForSeconds(moveUpDelay);

            start = Time.time;
            while(Time.time < start + moveDuration)
            {
                t = (Time.time - start)/moveDuration;
                angle = Mathf.Lerp(targetAngle, startAngle, t);
                root.localEulerAngles = new Vector3(0f, 0f, angle);
                yield return null;
            }
            yield return new WaitForSeconds(sequenceDelay);
        }
    }


}
