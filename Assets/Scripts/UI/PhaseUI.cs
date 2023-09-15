using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseUI : MonoBehaviour
{
    [SerializeField] private Image portraitUI;
    [SerializeField] private Image barUI;

    public Sprite playerPortrait
    {
        get { return portraitUI.sprite; }
        set { portraitUI.sprite = value; }
    }
    public float velocityBar
    {
        get { return barUI.fillAmount; }
        set { barUI.fillAmount = value; }
    }
}
