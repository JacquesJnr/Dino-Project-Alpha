using TMPro;

using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public TMP_Text healthText;

    void Update()
    {
        healthText.text = $"Health: {PlayerHealth.Instance.health}";
    }
}
