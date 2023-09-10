using System;
using System.Collections;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator anim;
    public static AnimationManager Instance;

    public void RunMode()
    {
        anim.SetBool("Run",true);
    }
    public void RollMode()
    {
        anim.SetTrigger("Roll");
        anim.SetBool("Run",false);
    }
    public void FlyMode()
    {
        anim.SetTrigger("Fly");
        anim.SetBool("Run",false);
    }
    
    private void Awake()
    {
        Instance = this;
        anim = GameObject.Find("Animator").GetComponent<Animator>();
    }
}