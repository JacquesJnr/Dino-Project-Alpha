using System;
using System.Collections;
using UnityEngine;

public class ManualStateSwap : MonoBehaviour
{
    public Animator anim;

    public void RunMode()
    {
        StateMachine.Instance.SetState(Mode.Running);
        anim.SetBool("Run",true);
    }

    public void RollMode()
    {
        StateMachine.Instance.SetState(Mode.Rolling);
        anim.SetTrigger("Roll");
        anim.SetBool("Run",false);
    }

    public void FlyMode()
    {
        StateMachine.Instance.SetState(Mode.Flying);
        anim.SetTrigger("Fly");
        anim.SetBool("Run",false);
    }
}
