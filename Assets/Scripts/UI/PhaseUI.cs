using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseUI : MonoBehaviour
{
    [SerializeField] private Image portraitUI;
    [SerializeField] private Image barUI;

    public float velocityBar
    {
        get { return barUI.fillAmount; }
        set { barUI.fillAmount = value; }
    }

    public static PhaseUI Instance;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        velocityBar = GameManager.Instance.gameSpeed;
    }

    public void SetPlayerPortrait(Sprite img)
    {
        portraitUI.sprite = img;
    }

    public void HitAlpha()
    {
        StartCoroutine(PingPong(0.5F));
    }

    public IEnumerator PingPong(float duration)
    {
        float time = 0;
        Color faded = new Color(1, 1, 1, 0);
        
        while (time < duration)
        {
            Color alpha = Color.Lerp(Color.white, faded, Mathf.PingPong(time / (duration * 0.5f),1));
            portraitUI.color = alpha;
            
            time += Time.deltaTime;
            yield return null;
        }
        
        portraitUI.color = Color.white;
    }
}
