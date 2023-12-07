using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject menuPage;
    public Button playButton;
    public Button controlsButton;
    public Button creditsButton;
    public Button quitButton;

    public GameObject instructionsPage;
    public GameObject creditsPage;
    public Button controlsBackButton;
    public Button creditsBackButton;

    void Start()
    {
      
        playButton.onClick.AddListener(PlayGame);
        controlsButton.onClick.AddListener(OpenOptions);
        creditsButton.onClick.AddListener(OpenCredits);
        quitButton.onClick.AddListener(QuitGame);
        controlsBackButton.onClick.AddListener(ReturnMainMenu);
        creditsBackButton.onClick.AddListener(ReturnMainMenu);

    }

   
    public void PlayGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    void OpenOptions()
    {
        menuPage.SetActive(false);
        instructionsPage.SetActive(true);


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
    public void QuitGame()
    {
        Application.Quit();

        
    }

  
}
