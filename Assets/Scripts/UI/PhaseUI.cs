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
}
