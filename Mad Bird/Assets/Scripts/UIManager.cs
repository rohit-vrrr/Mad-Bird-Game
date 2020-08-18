using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;                   // Singleton

    public TextMeshProUGUI scoreText;                   // TMPro scoreText
    public GameObject startUI;                          // gameUI
    public GameObject gameOverPanel;                    // gameOver Panel Animation
    public GameObject highScorePanel;                   // HighScorePanel

    public TextMeshProUGUI currentScoreText;            // gameOver Panel score text
    public TextMeshProUGUI highScoreText;               // gameOver Panel highscore text

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        scoreText.text = ScoreManager.instance.score.ToString();
    }

    public void GameStart()                 // When game is started startUI is deactivated
    {
        startUI.SetActive(false);
    }

    public void GameOver()                  // When game ends, GameOver Panel is activated
    {
        currentScoreText.text = "SCORE: " + PlayerPrefs.GetInt("Score");
        highScoreText.text = "BEST: " + PlayerPrefs.GetInt("HighScore");

        scoreText.alpha = 0;
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnHighScoreButtonClick()
    {
        highScorePanel.SetActive(true);
    }

    public void OnBackButtonClick()
    {
        GameObject.Find("HighScorePanel").GetComponent<Animator>().Play("HighScorePanelDown");
        highScorePanel.SetActive(false);
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("Menu");
    }
}
