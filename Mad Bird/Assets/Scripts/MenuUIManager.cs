using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public GameObject instructionPanel;

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnInstructionButtonClick()
    {
        instructionPanel.SetActive(true);
    }

    public void OnBackButtonClick()
    {
        instructionPanel.SetActive(false);
    }
}
