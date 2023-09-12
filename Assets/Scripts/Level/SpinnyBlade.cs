using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyBlade : MonoBehaviour
{
    public AnimationCurve windupCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    public AnimationCurve slashCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    public float slashDuration = 0.1f;
    public float windupDuration = 0.5f;
    public float windupAngle = 20f;
    public float sequenceDelay = 0.5f;

    [Range(-1f, 1f)] public int direction = 1;

    private const float SLASH_ANGLE = 120f;
    private const float OFFSET = 50f;

    void Start()
    {
        StartCoroutine(SpinnyRoutine());
    }

    private IEnumerator SpinnyRoutine()
    {
        float start, angle, t;
        int slashyIndex = 0;
        while(true)
        {
            start = Time.time;
            while(Time.time < start + windupDuration)
            {
                t = windupCurve.Evaluate((Time.time - start)/windupDuration);
                angle = slashyIndex*SLASH_ANGLE + t*windupAngle + OFFSET;
                angle *= direction;

                SetAngle(angle);
                yield return null;
            }

            start = Time.time;
            while(Time.time < start + slashDuration)
            {
                t = slashCurve.Evaluate((Time.time - start)/slashDuration);
                angle = slashyIndex*SLASH_ANGLE + t*SLASH_ANGLE + OFFSET;
                angle *= direction;

                SetAngle(angle);
                yield return null;
            }

            slashyIndex = (slashyIndex + 1) % 3;
            yield return new WaitForSeconds(sequenceDelay);
        }
    }

    void SetAngle(float angle)
    {


        transform.localEulerAngles = new Vector3(angle, 270f, 90f);
    }
}
