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
    public Transform hurtbox;

    void Start()
    {
        StartCoroutine(BladeRoutine());
    }

    private void Update()
    {
        // Keep hurtbox upright (it rotates along with the arm)
        Vector3 angle = hurtbox.transform.localEulerAngles;
        angle.z = -root.transform.localEulerAngles.z;
        hurtbox.transform.localEulerAngles = angle;
    }

    private IEnumerator BladeRoutine()
    {
        Vector3 euler;
        float start, angle, t;
        while(true)
        {
            start = Time.time;
            while(Time.time < start + moveDuration)
            {
                t = (Time.time - start)/moveDuration;
                euler = root.localEulerAngles;
                euler.z = Mathf.Lerp(startAngle, targetAngle, t);
                root.localEulerAngles = euler;
                yield return null;
            }

            start = Time.time;
            while(Time.time < start + spinDuration)
            {
                t = (Time.time - start)/spinDuration;
                euler = blade.localEulerAngles;
                euler.z = t*360f;
                blade.localEulerAngles = euler;
                yield return null;
            }
            yield return new WaitForSeconds(moveUpDelay);

            start = Time.time;
            while(Time.time < start + moveDuration)
            {
                t = (Time.time - start)/moveDuration;
                euler = root.localEulerAngles;
                euler.z = Mathf.Lerp(targetAngle, startAngle, t);
                root.localEulerAngles = euler;
                yield return null;
            }
            yield return new WaitForSeconds(sequenceDelay);
        }
    }


}
