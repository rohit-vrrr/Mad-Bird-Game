using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public float maxYPos;
    public float spawnTime;
    bool isFirst;

    public GameObject pipeLevel1;
    public GameObject pipeLevel2;

    private void Start()
    {
        isFirst = true;
    }

    public void StartSpawningPipes()
    {
        InvokeRepeating("SpawnPipe", 3f, spawnTime);            // Spawning after 3secs and 0.2secs consecutively
    }

    public void StopSpawningPipes()
    {
        CancelInvoke("SpawnPipe");
    }

    private void SpawnPipe()
    {
        if(ScoreManager.instance.score <= 48)
        {
            if (!isFirst)
            {
                Instantiate(pipeLevel1, new Vector3(transform.position.x,
                    Random.Range(-maxYPos, maxYPos), 0), Quaternion.identity);
            }
            else
            {
                Instantiate(pipeLevel1, new Vector3(transform.position.x, 0, 0), Quaternion.identity);
                isFirst = false;
            }
        }
        else if(ScoreManager.instance.score > 48)
        {
            float rand = Random.Range(1f, 11f);
            if(rand <=5)
            {
                Instantiate(pipeLevel1, new Vector3(transform.position.x,
                    Random.Range(-maxYPos, maxYPos), 0), Quaternion.identity);
            }
            else
            {
                Instantiate(pipeLevel2, new Vector3(transform.position.x, 
                    Random.Range(-maxYPos, maxYPos), 0), Quaternion.identity);
            }
        }
    }
}
