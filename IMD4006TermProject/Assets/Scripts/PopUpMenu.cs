using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopUpMenu : MonoBehaviour
{

    public GameObject menuPage;
    public Button pauseButton;
    public Button resumeButton;
    public Button controlsButton;
    public Button creditsButton;
    public Button quitButton;
    public GameObject menuPrefab;
    public GameObject instructionsPage;
    public GameObject creditsPage;
    public Button controlsBackButton;
    public Button creditsBackButton;
    //public GameObject Player;

    void Start()
    {
        
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(PlayGame);
        controlsButton.onClick.AddListener(OpenOptions);
        creditsButton.onClick.AddListener(OpenCredits);
        quitButton.onClick.AddListener(QuitGame);
        controlsBackButton.onClick.AddListener(ReturnMainMenu);
        creditsBackButton.onClick.AddListener(ReturnMainMenu);
        
        menuPrefab.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
           
        }

    }
 
public void PlayGame()
    {
        //continue the scene that was paused

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
        menuPrefab.SetActive(false);
        menuPage.SetActive(false);
        instructionsPage.SetActive(false);
        creditsPage.SetActive(false);
        Time.timeScale = 1;

    }
    void OpenOptions()
    {
        menuPage.SetActive(false);
        instructionsPage.SetActive(true);
        creditsPage.SetActive(false);


    }
    void OpenCredits()
    {
        menuPage.SetActive(false);
        instructionsPage.SetActive(false);
        creditsPage.SetActive(true);

    }
    void ReturnMainMenu()
    {
        instructionsPage.SetActive(false);
        creditsPage.SetActive(false);
        menuPage.SetActive(true);


    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        //Pause game
        //pop up aplication
        menuPrefab.SetActive(true);
        menuPage.SetActive(true);
        instructionsPage.SetActive(false);
        creditsPage.SetActive(false);
       Time.timeScale = (float)0.0001;
       
        

    }
    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


}

