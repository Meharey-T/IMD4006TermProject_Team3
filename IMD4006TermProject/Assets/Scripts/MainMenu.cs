using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject menuPage;
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;

    public GameObject instructionsPage;
    public Button backButton;

    void Start()
    {
      
        playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(OpenOptions);
        quitButton.onClick.AddListener(QuitGame);

        backButton.onClick.AddListener(ReturnMainMenu);

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
    void ReturnMainMenu()
    {
        instructionsPage.SetActive(false);
        menuPage.SetActive(true);
    

    }
    public void QuitGame()
    {
        Application.Quit();

        
    }

  
}
