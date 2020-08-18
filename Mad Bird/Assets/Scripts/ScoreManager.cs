using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;            // Singleton

    public HighScoreTable highscoretable;
    public int score;
    bool isTableUpdated;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        score = 0;
        isTableUpdated = false;

        PlayerPrefs.SetInt("Score", 0);
    }

    public void IncrementScore()
    {
        score += 1;
    }

    public void StopScore()         // Setting HighScore
    {
        PlayerPrefs.SetInt("Score", score);         // Current Score
        if(score >= 1 && !isTableUpdated)
        {
            highscoretable.AddHighScoreEntry(score);
            isTableUpdated = true;
        }

        if (PlayerPrefs.HasKey("HighScore"))         // Checking if HighScore is already present
        {
            if(score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
