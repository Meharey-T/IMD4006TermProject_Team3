using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * To set the Player prefab up please do the following:
 * Drag the "CoinCount" text object from the UI to the "Coin Count Txt" slot on this script
 * Drag the "CentralText" text object from the UI to the "Central Txt" slot on this script
 * Drag the "Lives Panel" game object from the UI to the "P_Lives Count" slot on this script
 * Drag the "DetectionIcon" game object from the UI to the "Indicator" slot on this script
 * It should run. I will see in the future if I can actually make this simpler.
 */
//Player script holds the model (stats) and main functionality for Player
public class Player : MonoBehaviour
{
    private Vector3 startingPos;

    //Reference to our manager objects/lists
    public GameObjectRuntimeSet coinSet;
    public GameObjectRuntimeSet enemySet;
    public GameObject trapObj;
    public GameObject colliderHolder;

    public TMPro.TextMeshProUGUI coinCountTxt;
    public TMPro.TextMeshProUGUI centralTxt;
    //public TMPro.TextMeshProUGUI indicator;
    public GameObject P_LivesCount;
    public GameObject indicator;
    public bool winLose;
    [SerializeField] public int coinCount = 0;
    private int totalCoinCount;

    [SerializeField] GameObject dragonImage;
    [SerializeField] private Image treasureBar;

    int lives = 3;

    [SerializeField] Texture i_health;
    [SerializeField] Texture i_hurt;

    [SerializeField] Texture i_unseen;
    [SerializeField] Texture i_seen;
    [SerializeField] Texture i_spotted;
    [SerializeField] Texture i_unheard;
    [SerializeField] Texture i_heard;
    [SerializeField] Texture i_beingHeard;

    
    [SerializeField] Texture i_unimpressed;
    [SerializeField] Texture i_pleased;

    public bool inRangeOfHideable = false;
    public bool hiding = false;
    public Vector3 selectedHideable;
    public Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        coinCountTxt.text = "Coin Count: 0";
        centralTxt.enabled = false;
        previousPosition = new Vector3(0, 0, 0);
        startingPos = this.transform.position;
        CalcTotalTreasureToSteal();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            SetTrap();
        }
        UpdateLives();
        CheckDetection();
        RecalculateTreasureStolen();
    }

    void CheckDetection()
    {
        //If the player is hiding, we need to show the undetectable icon
        if (this.GetComponent<BaseStateMachine>().CurrentState.name != "Hiding")
        {
            indicator.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            indicator.transform.GetChild(1).gameObject.SetActive(true);
        }
        //Resets all of our detection states
        bool playerIsHeard = false;
        bool playerWasHeard = false;
        bool playerIsSeen = false;
        bool playerWasSeen = false;
        //We have to iterate through all enemies
        for (int i = 0; i < enemySet.Items.Count; i++)
        {
            //Check player seen status
            if (enemySet.Items[i].GetComponent<Enemy>().hearsPlayer)
            {
                playerIsHeard = true;
            }
            else if (enemySet.Items[i].GetComponent<Enemy>().heardPlayer)
            {
                playerWasHeard = true;
            }

            //Check player heard status
            if (enemySet.Items[i].GetComponent<Enemy>().seesPlayer)
            {
                playerIsSeen = true;
            }
            else if (enemySet.Items[i].GetComponent<Enemy>().sawPlayer)
            {
                playerWasSeen = true;
            }

        }
        //If we've gone through the whole loop and nobody detects the player, we set to undetected
        if (!playerIsSeen && !playerWasSeen)
        {
            indicator.transform.GetChild(0).GetComponent<RawImage>().texture = i_unseen;
        }
        else if (!playerIsSeen && playerWasSeen)
        {
            indicator.transform.GetChild(0).GetComponent<RawImage>().texture = i_seen;
        }
        if (playerIsSeen)
        {
            indicator.transform.GetChild(0).GetComponent<RawImage>().texture = i_spotted;
        }
        //if at least one heard the player, we can set to heard
        if (!playerIsHeard && !playerWasHeard)
        {
            indicator.transform.GetChild(2).GetComponent<RawImage>().texture = i_unheard;
        }
        else if (!playerIsHeard && playerWasHeard)
        {
            indicator.transform.GetChild(2).GetComponent<RawImage>().texture = i_heard;
        }
        else if (playerIsHeard)
        {
            indicator.transform.GetChild(2).GetComponent<RawImage>().texture = i_beingHeard;
        }

    }

    private void CalcTotalTreasureToSteal()
    {
        for (int i = 0; i < coinSet.Items.Count; i++)
        {
            //Go through all treasure and then subtract by final chest amount since it's not going to affect gameplay
            totalCoinCount += coinSet.Items[i].GetComponent<Treasure>().treasureStats.coinValue;
        }
    }

    private void RecalculateTreasureStolen()
    {
        float treasurePercent = (float)coinCount / totalCoinCount;
        treasureBar.fillAmount = treasurePercent;
        if(coinCount > totalCoinCount / 2)
        {
            dragonImage.transform.GetChild(0).GetComponentInChildren<RawImage>().texture = i_pleased;
        }
        else
        {
            dragonImage.transform.GetChild(0).GetComponentInChildren<RawImage>().texture = i_unimpressed;
        }
    }

    void UpdateLives()
    {
        int livesMissing = 3 - lives;
        for (int i = 0; i < 3; i++)
        {
            P_LivesCount.transform.GetChild(i).GetComponentInChildren<RawImage>().texture = i_health;
        }
        if (livesMissing > 0)
        {
            for (int i = livesMissing; i > 0; i--)
            {
                if (i<0)
                { this.gameObject.layer = 8; }
                P_LivesCount.transform.GetChild(3 - i).GetComponentInChildren<RawImage>().texture = i_hurt;

            }
        }
    }

    public void OnPlayerLoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            StartCoroutine(OnPlayerLoss());

        }
        else
        {
            ResetPositions();
        }
    }

    private void ResetPositions()
    {
        this.transform.position = startingPos;
        foreach (GameObject enemy in enemySet.Items)
        {
            enemy.transform.position = enemy.GetComponent<Enemy>().startingPos;
            enemy.transform.rotation = new Quaternion(0, 0, 0, 1);
            enemy.GetComponent<Enemy>().seesPlayer = false;
            enemy.GetComponent<Enemy>().sawPlayer = false;
            enemy.GetComponent<Enemy>().hearsPlayer = false;
            enemy.GetComponent<Enemy>().heardPlayer = false;
            enemy.GetComponent<Enemy>().hasStopped = false;
            enemy.GetComponent<Enemy>().caughtPlayer = true;
        }
        //StartCoroutine(ReactivateEnemies());
    }

    IEnumerator ReactivateEnemies()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject enemy in enemySet.Items)
        {
            enemy.GetComponent<Enemy>().caughtPlayer = false;
        }
    }

    private void SetTrap()
    {
        Instantiate(trapObj, (gameObject.transform.position), gameObject.transform.rotation);

    }

    public void SetWinLose ( bool iswinner)
    {
        winLose=iswinner;
    }
    public IEnumerator OnPlayerLoss()
    {
       this.gameObject.layer = 8;
        centralTxt.text = "You got kicked out!";
        centralTxt.enabled = true;
        SetWinLose(false);
     
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(2);
    }

    public IEnumerator OnPlayerWon()
    {
        this.gameObject.layer = 8;
        centralTxt.text = "You won! \n You got " + coinCount + " coins!";
        centralTxt.enabled = true;
        SetWinLose(true);
        
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(3);
    }
}
