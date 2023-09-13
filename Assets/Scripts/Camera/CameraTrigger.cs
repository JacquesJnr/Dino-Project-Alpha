using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Mode targetMode;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (transform.name.Contains("State"))
            {
                StateMachine.Instance.SetState(targetMode);
            }
        }
    }
}
