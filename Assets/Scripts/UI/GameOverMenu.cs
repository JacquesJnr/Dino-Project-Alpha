using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Image gameOverImage;
    public TMP_Text header, scoreText, killerName, killerInfo;
    public Button restartButton, continueButton;
    public Sprite teslaKill, sawKill, enemyKill;
    public Image killerIcon;

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

    public void SetKillInfo(string name)
    {
        if (name.Contains("Hurtbox")) // This is called hurtbox on the prefab
        {
            killerName.text = "Saw Blade";
            killerInfo.text = "Hint: You can dash while running to avoid the blades!";
            killerIcon.sprite = sawKill;
        }

        if (name.Contains("TeslaCoil"))
        {
            killerName.text = "Tesla Coil";
            killerInfo.text = "You got zapped!";
            killerIcon.sprite = teslaKill;
        }

        if (name.Contains("Scientist"))
        {
            killerName.text = "Evil Scientist";
            killerInfo.text = "Hint: Speed up to send these guys flying!";
            killerIcon.sprite = enemyKill;
        }
    }
}
