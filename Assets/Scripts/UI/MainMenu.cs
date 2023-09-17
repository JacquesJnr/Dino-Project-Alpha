using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartGameButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/DINOJAM", LoadSceneMode.Single);
    }
}
