using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("HSEntryContainer");
        entryTemplate = entryContainer.Find("HSEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        // AddHighScoreEntry(1);

        string jsonString = PlayerPrefs.GetString("HighScoreTable");                                // 1- Load json string from PlayerPrefs
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);                       // 2- Convert into a HighScores object

        if(highscores != null)
        {
            /* Sorting entry list By Score */
            for (int i = 0; i < highscores.highScoreEntryList.Count; i++)                           // 3- Sort the list
            {
                for (int j = i + 1; j < highscores.highScoreEntryList.Count; j++)
                {
                    if (highscores.highScoreEntryList[j].score > highscores.highScoreEntryList[i].score)
                    {
                        // Swap
                        HighScoreEntry temp = highscores.highScoreEntryList[i];
                        highscores.highScoreEntryList[i] = highscores.highScoreEntryList[j];
                        highscores.highScoreEntryList[j] = temp;
                    }
                }
            }

            highScoreEntryTransformList = new List<Transform>();

            foreach (HighScoreEntry highScoreEntry in highscores.highScoreEntryList)                  // 4- Create EntryTransform
            {
                CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList);
            }
        }
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 45f;

        if(transformList.Count<=9)
        {
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
            entryTransform.gameObject.SetActive(true);

            /* Rank Field*/
            int rank = transformList.Count + 1;
            string rankString;
            switch (rank)                                                                       // converting rank to string
            {
                default:
                    rankString = rank + "TH"; break;

                case 1: rankString = "1ST"; break;
                case 2: rankString = "2ND"; break;
                case 3: rankString = "3RD"; break;
            }

            entryTransform.Find("Index").GetComponent<TextMeshProUGUI>().text = rankString;

            /* Score Field*/
            int score = highscoreEntry.score;
            entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();

            // Setting Score Background visible odds and evens
            entryTransform.Find("Score Background").gameObject.SetActive(rank % 2 == 1);

            if (rank == 1)
            {
                // Highlighting First
                entryTransform.Find("Index").GetComponent<TextMeshProUGUI>().color = Color.yellow;
                entryTransform.Find("Score").GetComponent<TextMeshProUGUI>().color = Color.yellow;
            }

            transformList.Add(entryTransform);                                                  // Adding the values into the table
        }
    }


    /*
     * Adding new entry
     * */
    public void AddHighScoreEntry(int score)
    {
        // Create HighScoreEntry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score };

        // Load saved HighScores
        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        HighScores highscores = JsonUtility.FromJson<HighScores>(jsonString);

        if (highscores == null)
        {
            // If there is no stored table
            highscores = new HighScores() { 
                highScoreEntryList = new List<HighScoreEntry>() };
        }

        // Add new entry to HighScores
        highscores.highScoreEntryList.Add(highScoreEntry);

        // Save Updated HighScores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighScoreTable", json);
        PlayerPrefs.Save();
    }


    /*
     * Class for the json to save
     * */
    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntryList;
    }


    /*
     * Represents a single High Score entry
     * */
    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
    }
}
