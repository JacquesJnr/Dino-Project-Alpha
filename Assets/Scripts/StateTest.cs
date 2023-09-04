using System;
using System.Collections;
using UnityEngine;

public class StateTest : MonoBehaviour
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
        if (Input.GetKey(one))
        {
            StateMachine.Instance.SetState(Mode.Running);
            anim.SetTrigger("Run");
        }
        if (Input.GetKey(two))
        {
            StateMachine.Instance.SetState(Mode.Rolling);
            anim.SetTrigger("Roll");
        }
        if (Input.GetKey(three))
        {
            StateMachine.Instance.SetState(Mode.Flying);
            anim.SetTrigger("Fly");
            
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
