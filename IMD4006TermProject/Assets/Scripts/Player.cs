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
    public GameObject colliderHolder;

    public TMPro.TextMeshProUGUI coinCountTxt;
    public TMPro.TextMeshProUGUI centralTxt;
    //public TMPro.TextMeshProUGUI indicator;
    public GameObject P_LivesCount;
    public GameObject indicator;

    [SerializeField]public  int coinCount = 0;

    int lives = 3;

    [SerializeField] Texture i_health;
    [SerializeField] Texture i_hurt;

    [SerializeField] Texture i_undetected;
    [SerializeField] Texture i_alerted;
    [SerializeField] Texture i_spotted;
    [SerializeField] Texture i_undetectable;

    public bool inRangeOfHideable = false;
    public Vector3 selectedHideable;
    public Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        coinCountTxt.text = "Coin Count: 0";
        centralTxt.enabled = false;
        previousPosition = new Vector3(0, 0, 0);
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
        CheckDetection();
        
    }

    void CheckDetection()
    {
        if(this.GetComponent<BaseStateMachine>().CurrentState.name != "Hiding")
        {
            bool playerIsHeard = false;
            bool playerIsSeen = false;
            //We have to iterate through all enemies
            for (int i = 0; i < enemySet.Items.Count; i++)
            {
                if (playerIsSeen)
                {
                    break;
                }
                if (enemySet.Items[i].GetComponent<Enemy>().hearsPlayer)
                {
                    playerIsHeard = true;
                }
                if (enemySet.Items[i].GetComponent<Enemy>().seesPlayer)
                {
                    playerIsSeen = true;
                }
            }
            if(!playerIsHeard && !playerIsSeen)
            {
                indicator.transform.GetComponent<RawImage>().texture = i_undetected;
            }
            if (playerIsHeard)
            {
                indicator.transform.GetComponent<RawImage>().texture = i_alerted;
            }
            if (playerIsSeen)
            {
                indicator.transform.GetComponent<RawImage>().texture = i_spotted;
            }
        }
        else
        {
            indicator.transform.GetComponent<RawImage>().texture = i_undetectable;
        }
        
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

    public IEnumerator OnPlayerLoss()
    {
        centralTxt.text = "You died";
        centralTxt.enabled = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator OnPlayerWon()
    {
        centralTxt.text = "You won! \n You got " + coinCount + " coins!";
        centralTxt.enabled = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
