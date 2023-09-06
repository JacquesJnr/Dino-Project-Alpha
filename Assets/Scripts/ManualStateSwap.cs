using System;
using System.Collections;
using UnityEngine;

public class ManualStateSwap : MonoBehaviour
{
    private KeyCode one, two, three;
    public Animator anim;

    private void Start()
    {
        one = KeyCode.Alpha1;
        two = KeyCode.Alpha2;
        three = KeyCode.Alpha3;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(one))
        {
            StateMachine.Instance.SetState(Mode.Running);
            anim.SetBool("Run",true);
        }
        if (Input.GetKeyDown(two))
        {
            StateMachine.Instance.SetState(Mode.Rolling);
            anim.SetTrigger("Roll");
            anim.SetBool("Run",false);
        }
        if (Input.GetKeyDown(three))
        {
            StateMachine.Instance.SetState(Mode.Flying);
            anim.SetTrigger("Fly");
            anim.SetBool("Run",false);
        }
        
        SetAnimState();
    }
    
    public void SetAnimState()
    {
        var mode = StateMachine.Instance.GetState();
        switch (mode)
        {
            case Mode.Running:
                break;
            case Mode.Rolling:
                break;
            case Mode.Flying:
                break;
        }
    }
}
