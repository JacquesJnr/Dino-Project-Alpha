using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("Bend config")]
    [Range(-0.015f, 0.015f)]
    public float bendRange;
    public float bendDuration = 1f;
    [NonSerialized] public float bend;
    
    [Header("CONTROLLERS")]
    public PlayerController runningControls;
    public Roller rollingControls;
    public Flight flyingControls;
    public bool grounded;

    [Header("VELOCITY PARAMS")]
    [Range(0,1)]public float gameSpeed;
    public float acceleration;
    public AnimationCurve curve;
    public float delay;
    private bool waiting;
    [Range(0,1)] public float hitDecrease;
    public event Action OnPhaseIncreased;
    public event Action OnPhaseDecreased;
    
    [Header("GAME OVER / PAUSE MENU")]
    public bool godMode;
    public bool dead;
    public GameOverMenu gameOverMenu;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Sfx.Instance.Play(Sfx.Instance.startGameSiren, 1f, 1);
        StateMachine.Instance.OnStateChanged += OnStateChanged;
        PlayerHealth.OnPlayerHit += OnPlayerHit;
        OnStateChanged();
    }

    public void OnStateChanged()
    {
        SetGameControls();
    }
    
    public void OnPlayerHit()
    {
        Sfx.Instance.Play(Sfx.Instance.playerCollision);
        OnPhaseDecreased?.Invoke();
        if (gameSpeed >= hitDecrease)
        {
            gameSpeed -= hitDecrease;
        }
        else
        {
            if (!godMode)
            {
                Debug.Log("You are Dead!");
                gameOverMenu.ShowGameOverMenu();
                dead = true;
            }
        }
    }

    public void SetGameControls()
    {
        switch (StateMachine.Instance.GetState())
        {
            case Mode.Running:
                runningControls.enabled = true;
                rollingControls.enabled = false;
                flyingControls.enabled = false;
                
                grounded = !PlayerController.Instance.isJumping;
                break;
            case Mode.Rolling:
                rollingControls.enabled = true;
                runningControls.enabled = false;
                flyingControls.enabled = false;
                
                grounded = true;
                break;
            case Mode.Flying:
                runningControls.enabled = false;
                rollingControls.enabled = false;
                flyingControls.enabled = true;
                
                grounded = false;
                break;
        }
    }
    
    private void Update()
    {
        // World Bend
        float t = Time.time/bendDuration;
        bend = Mathf.Sin(t*2f*Mathf.PI)*bendRange;
        Shader.SetGlobalFloat("_Curvature", bend);

        // Increase Game Speed
        if(gameSpeed < 1)
        {
            if (waiting)
            {
                return;
            }

            if (gameSpeed >= 0.33F && gameSpeed < 0.34F)
            {
                StartCoroutine(VelocityDelay(0.33f,delay));
            }
            
            if (gameSpeed >= 0.66F && gameSpeed < 0.67F)
            {
                StartCoroutine(VelocityDelay(0.66f,delay));
            }
            
            gameSpeed += acceleration * curve.Evaluate(Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameOverMenu.isActiveAndEnabled)
            {
                gameOverMenu.OnContinueButtonPressed();
            }
            else
            {
                gameOverMenu.ShowPauseMenu();
            }
        }
    }

    private IEnumerator VelocityDelay(float amount ,float duration)
    {
        waiting = true;
        yield return new WaitForSeconds(duration);
        gameSpeed = amount + 0.01f;
        waiting = false;
        OnPhaseIncreased?.Invoke();
    }
}
