using TMPro;

using UnityEngine;

public class RandomHint : MonoBehaviour
{
    [TextArea]
    public string[] hints;
    public TMP_Text text;

    private void OnEnable()
    {
        text.text = hints.Choose();
    }
}
