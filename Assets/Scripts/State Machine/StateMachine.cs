using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance;
    public event Action OnStateChanged;

    private Mode playerMode { get; set; }

    [SerializeField] private Mode currentState;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetState(Mode newState)
    {
        playerMode = newState;
        OnStateChanged?.Invoke();
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
