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
    public float randomStartDelay = 3f;

    [Header("References")]
    public Transform root;
    public Transform blade;
    public Transform hurtbox;
    public AudioSource spinningSound;

    void Start()
    {
        StartCoroutine(BladeRoutine());
    }

    private void Update()
    {
        // Keep hurtbox upright (it rotates along with the arm)
        Vector3 angle = hurtbox.transform.localEulerAngles;
        angle.x = -root.transform.localEulerAngles.x;
        hurtbox.transform.localEulerAngles = angle;
    }

    private IEnumerator BladeRoutine()
    {
        yield return new WaitForSeconds(Random.value*randomStartDelay);

        Vector3 euler;
        float start, t;
        while(true)
        {
            start = Time.time;
            while(Time.time < start + moveDuration)
            {
                t = (Time.time - start)/moveDuration;
                euler = root.localEulerAngles;
                euler.x = Mathf.Lerp(startAngle, targetAngle, t);
                root.localEulerAngles = euler;
                yield return null;
            }

            start = Time.time;
            while(Time.time < start + spinDuration)
            {
                spinningSound.Play();
                t = (Time.time - start)/spinDuration;
                euler = blade.localEulerAngles;
                euler.x = -t*540f;
                blade.localEulerAngles = euler;
                yield return null;
            }
            spinningSound.Stop();
            yield return new WaitForSeconds(moveUpDelay);

            start = Time.time;
            while(Time.time < start + moveDuration)
            {
                t = (Time.time - start)/moveDuration;
                euler = root.localEulerAngles;
                euler.x = Mathf.Lerp(targetAngle, startAngle, t);
                root.localEulerAngles = euler;
                yield return null;
            }
            yield return new WaitForSeconds(sequenceDelay);
        }
    }


}
