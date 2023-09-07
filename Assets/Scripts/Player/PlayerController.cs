using System;
using System.Collections;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int indexedPosition;
    public int lastInput;
    public float laneWidth = 1f;
    public float swapSpeed = 0.5f;
    public float dashDuration;
    public float dashSpeedMultiplier;

    public float speed = 1f;

    public event Action StartTileSwitch;
    public event Action FinishTileSwitch;

    private Coroutine swapRoutine;
    private Coroutine dashRoutine;

    private Rigidbody rb => GetComponentInChildren<Rigidbody>();

    public static PlayerController Instance;

    private void Start()
    {
        Instance = this;
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        if(input != 0f)
        {
            int direction = Mathf.RoundToInt((float)Mathf.Sign(input));
            if(direction != lastInput)
            {
                int currentPosition = indexedPosition;
                int targetPosition = Mathf.Clamp(indexedPosition + direction, -1, 1);
                if(swapRoutine == null && currentPosition != targetPosition)
                {
                   swapRoutine = StartCoroutine(MoveToTile(currentPosition, targetPosition));
                }
            }
            lastInput = direction;
        }
        else
        {
            lastInput = 0;
        }
        
        // Changed Key to 'E'
        if(Input.GetKeyDown(KeyCode.E) && dashRoutine == null)
        {
            dashRoutine = StartCoroutine(Dash());
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

        indexedPosition = to;
        currentPos = transform.localPosition;
        currentPos.x = to*laneWidth;
        transform.localPosition = currentPos;
        swapRoutine = null;
    }

    IEnumerator Dash()
    {
        speed = dashSpeedMultiplier;
        yield return new WaitForSeconds(dashDuration);
        speed = 1f;

        dashRoutine = null;
    }
   
}
