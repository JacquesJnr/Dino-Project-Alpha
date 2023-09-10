using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float duration = 1f;
    public new Light light;

    void Update()
    {
        light.intensity = curve.Evaluate(Time.time/duration);
    }
}
