
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WinScreenUI : MonoBehaviour
{

    public Button restartGameButton;
    public Button mainMenuButton;




    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        restartGameButton.interactable=true;
        mainMenuButton.interactable = true;
        restartGameButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnMainMenu);

    }


    public void RestartGame()
    {

        SceneManager.LoadScene(1);

    }
    void ReturnMainMenu()
    {

        SceneManager.LoadScene(0);


    }
}

    /*
        if (isWinner)
        {
            SetWin();
           
        }
        else if (!isWinner)
        {
            SetLose();
        }
       
    }


    public void RestartGame()
    {

        SceneManager.LoadScene(1);

    }
  
    void SetWin()
    {
        winPage.SetActive(true);
        losePage.SetActive(false);
        restartGameButton = winButton[(int)0];
        mainMenuButton = winButton[(int)1];
        SetButtons();
    }
    void SetLose()
    {
        losePage.SetActive(true);
        winPage.SetActive(false);
        restartGameButton = loseButton[(int)0];
        mainMenuButton = loseButton[(int)1];
        SetButtons();
    }
    void SetButtons()
    {
        restartGameButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnMainMenu);
    }
    void ReturnMainMenu()
    {

        SceneManager.LoadScene(0);


    }
    public void QuitGame()
    {
        Application.Quit();


    }


}
        */