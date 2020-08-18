using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameOver;

    private void Awake()
    {
        // DontDestroyOnLoad(this.gameObject);                  // Dont destroy GameManager

        if(instance == null)                                    // If instance != null, there is another GameManager
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);                           // If there exists another GameManager, then Destroy this
        }
    }

    void Start()
    {
        gameOver = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        UIManager.instance.GameStart();

        GameObject.Find("Pipe Spawner").GetComponent<PipeSpawner>().StartSpawningPipes();
    }

    public void GameOver()
    {
        gameOver = true;

        GameObject.Find("Pipe Spawner").GetComponent<PipeSpawner>().StopSpawningPipes();
        ScoreManager.instance.StopScore();

        AdManager.instance.PlayInterstitialAd();

        UIManager.instance.GameOver();
    }
}
