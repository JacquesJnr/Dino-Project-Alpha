using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int indexedPosition;
    public int lastInput;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        if(input != 0f)
        {
            int direction = Mathf.RoundToInt((float)Mathf.Sign(input));
            if(direction != lastInput)
            {
                indexedPosition = Mathf.Clamp(indexedPosition + direction, -1, 1);
                var currentPos = transform.localPosition;
                currentPos.x = indexedPosition*5f;
                transform.localPosition = currentPos;
            }
            lastInput = direction;
        }
        else
        {
            lastInput = 0;
        }
    }
}
