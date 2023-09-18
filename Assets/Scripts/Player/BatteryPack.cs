using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPack : MonoBehaviour
{
    public float fadeTime = 0.5F;
    private SpriteRenderer sp;

    public static BatteryPack Instance;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        Instance = this;
    }

    public void SetBatteryColor(Color phaseColor)
    {
        StartCoroutine(ColorChange(phaseColor));
    }

    public IEnumerator ColorChange(Color end)
    {
        float time = 0;

        while (time < fadeTime)
        {
            Color newColor = Color.Lerp(sp.color, end, time / fadeTime);

            sp.color = newColor;
            time += Time.deltaTime;
            yield return null;
        }

        sp.color = end;
    }

    private void Update()
    {
        sp.size = new Vector2(sp.size.x, GameManager.Instance.gameSpeed);
    }
}
