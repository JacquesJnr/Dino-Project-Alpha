using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Image gameOverImage;
    public TMP_Text header, scoreText;
    public Button restartButton, continueButton;

    public Sprite happyDino, deadDino;

    public void ShowGameOverMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        scoreText.text = $"Score\n{Score.Instance.score}";

        header.text = "Game\nover";
        scoreText.gameObject.SetActive(true);
        gameOverImage.sprite = deadDino;
        restartButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;

        header.text = "Game\npaused";
        scoreText.gameObject.SetActive(false);
        gameOverImage.sprite = happyDino;
        restartButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
    }

    public void OnRestartButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.dead = false;
    }

    public void OnContinueButtonPressed()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
