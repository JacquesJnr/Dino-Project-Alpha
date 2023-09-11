using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Note:
    // For now this script is disabled during states other than run to support the other movement types
    public float speed = 1f;

    [Header("Lanes")]
    public float laneWidth = 1f;
    public float swapSpeed = 0.5f;

    [Header("Dashing")]
    public float dashDuration;
    public float dashSpeedMultiplier = 2f;
    public float dashCooldown;

    public event Action StartTileSwitch;
    public event Action FinishTileSwitch;

    public int LaneIndex { get; set; }

    private int _lastInput;
    private float _lastVerticalInput;
    private float _nextDashTimer;

    private Coroutine swapRoutine;
    private Coroutine dashRoutine;
    
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
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
        if(Time.time > _nextDashTimer && _lastVerticalInput <= 0f && verticalInput > 0f && dashRoutine == null)
        {
            dashRoutine = StartCoroutine(Dash());
        }

        _lastVerticalInput = verticalInput;
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
        float originalSpeed = speed;
        speed = originalSpeed*dashSpeedMultiplier;

        yield return new WaitForSeconds(dashDuration);

        speed = originalSpeed;
        dashRoutine = null;
        _nextDashTimer = Time.time + dashCooldown;
    }
   
}
