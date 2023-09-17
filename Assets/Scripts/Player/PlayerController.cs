using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    // Note:
    // For now this script is disabled during states other than run to support the other movement types
    //[SerializeField] private float speed = 1f;
    //public float Speed => speed*PlayerHealth.Instance.GetSlowFactor();

    [Header("Lanes")]
    public float laneWidth = 1f;
    public float swapSpeed = 0.5f;

    [Header("Dashing")]
    public float dashDuration;
    public float dashSpeedMultiplier = 2f;
    public float dashCooldown;

    [Header("Jumping")]
    public bool isJumping;
    public float jumpDuration = 1f;
    public float jumpHeight = 2f;
    public AnimationCurve jumpCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    public event Action StartTileSwitch;
    public event Action FinishTileSwitch;
    public event Action JumpStarted;
    public event Action JumpEnded;

    public int LaneIndex { get; set; }

    private int _lastInput;
    private float _lastVerticalInput;
    private float _nextDashTimer;

    private Coroutine swapRoutine;
    private Coroutine dashRoutine;
    
    public static PlayerController Instance;

    public float DashSpeedMultipiplier { get; private set; } = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        AnimationManager.Instance.RunMode();
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        if(input != 0f)
        {
            int direction = Mathf.RoundToInt((float)Mathf.Sign(input));
            if(direction != _lastInput)
            {
                int currentPosition = LaneIndex;
                int targetPosition = Mathf.Clamp(LaneIndex + direction, -1, 1);
                if(swapRoutine == null && currentPosition != targetPosition)
                {
                   swapRoutine = StartCoroutine(MoveToTile(currentPosition, targetPosition));
                }
            }
            _lastInput = direction;
        }
        else
        {
            _lastInput = 0;
        }

        float verticalInput = Input.GetAxis("Vertical");
        if(Time.time > _nextDashTimer && _lastVerticalInput <= 0f && verticalInput > 0f && dashRoutine == null && !isJumping)
        {
            dashRoutine = StartCoroutine(Dash());
        }

        _lastVerticalInput = verticalInput;

        if(Input.GetKeyDown(KeyCode.Space) && !isJumping && dashRoutine == null)
        {
            StartCoroutine(Jump());
        }
    }

    IEnumerator MoveToTile(int from, int to)
    {
        StartTileSwitch?.Invoke();

        float startX = from*laneWidth;
        float endX = to*laneWidth;
        float start = Time.time;
        Vector3 currentPos;

        while(Time.time < start + swapSpeed)
        {
            float t = (Time.time - start)/swapSpeed;
            float xPos = Mathf.SmoothStep(startX, endX, t);
            currentPos = transform.localPosition;
            currentPos.x = xPos;
            transform.localPosition = currentPos;
            yield return null;
        }

        LaneIndex = to;
        currentPos = transform.localPosition;
        currentPos.x = to*laneWidth;
        transform.localPosition = currentPos;
        swapRoutine = null;
    }

    IEnumerator Dash()
    {
        DashSpeedMultipiplier = dashSpeedMultiplier;
        yield return new WaitForSeconds(dashDuration);
        DashSpeedMultipiplier = 1f;
        dashRoutine = null;
        _nextDashTimer = Time.time + dashCooldown;
    }

    IEnumerator Jump()
    {
        float start = Time.time;
        isJumping = true;
        AnimationManager.Instance.Jumping();

        while(Time.time < start + jumpDuration)
        {
            float t = jumpCurve.Evaluate((Time.time - start)/jumpDuration);
            Vector3 position = transform.position;
            position.y = jumpHeight*t;
            transform.position = position;
            yield return null;
        }

        AnimationManager.Instance.StopJumping();
        isJumping = false;
    }

}
