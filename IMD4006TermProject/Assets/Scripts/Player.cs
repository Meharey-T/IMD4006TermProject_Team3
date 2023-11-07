using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Player script holds the model (stats) and main functionality for Player
public class Player : MonoBehaviour
{
    //Reference to our manager objects/lists
    public GameObjectRuntimeSet coinSet;
    public GameObjectRuntimeSet enemySet;
    public GameObject trapObj;
    

    public TMPro.TextMeshProUGUI coinCountTxt;
    public TMPro.TextMeshProUGUI centralTxt;
    public GameObject P_LivesCount;

    [SerializeField] int coinCount = 0;

    int lives = 3;

    [SerializeField] Texture i_health;
    [SerializeField] Texture i_hurt;

    // Start is called before the first frame update
    void Start()
    {
        coinCountTxt.text = "Coin Count: 0";
        centralTxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
           SetTrap();
            Debug.Log("setTrap");
        }
        UpdateLives();
        
    }

    void UpdateLives()
    {
        int livesMissing = 3 - lives;
        for(int i = 0; i < 3; i++)
        {
            P_LivesCount.transform.GetChild(i).GetComponentInChildren<RawImage>().texture = i_health;
        }
        for(int i = livesMissing; i > 0; i--)
        {
            P_LivesCount.transform.GetChild(3-i).GetComponentInChildren<RawImage>().texture = i_hurt;
        }
    }

    //Check for collisions
    private void OnTriggerEnter(Collider other)
    {
        //Coin pickup
        if(other.gameObject.tag == "Treasure")
        {
            Debug.Log("Found treasure");
            //Figure out what kind of treasure we just found, act accordingly
            coinCount += other.GetComponent<Treasure>().treasureStats.coinValue;
            if (other.GetComponent<Treasure>().treasureStats.finalChest)
            {
                Debug.Log("Player has won");
                StartCoroutine(OnPlayerWon());
            }
            //Remove the coin from the scene
            other.GetComponent<Interactable>().Die();
            coinCountTxt.text = "Coin Count: " + coinCount;
        }

        if(other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Hit an obstacle");
            lives--;
            if (lives <= 0)
            {
                Debug.Log("Player has lost");
                StartCoroutine(OnPlayerLoss());
            }
        }
    }

    public void OnPlayerLoseLife()
    {
        Debug.Log("Hit an Enemy");
        lives--;
        if (lives <= 0)
        {
            Debug.Log("Player has lost a life");
            StartCoroutine(OnPlayerLoss());
        }
    }

    private void SetTrap()
    {
        Instantiate(trapObj, (gameObject.transform.position), gameObject.transform.rotation);
    
    }

    IEnumerator OnPlayerLoss()
    {
        centralTxt.text = "You died";
        centralTxt.enabled = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator OnPlayerWon()
    {
        centralTxt.text = "You won! \n You got " + coinCount + " coins!";
        centralTxt.enabled = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
