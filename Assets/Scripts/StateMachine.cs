using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Mode playerMode { get; set; }
    public static StateMachine Instance;
    [SerializeField] private Mode currentState;

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetState(Mode newState)
    {
        playerMode = newState;
    }

    public Mode GetState()
    {
        return playerMode;
    }

    private void Update()
    {
        currentState = playerMode;
    }
}
