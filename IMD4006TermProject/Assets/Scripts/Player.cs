using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Player script holds the model (stats) and main functionality for Player
public class Player : MonoBehaviour
{
    //Reference to our manager objects/lists
    public GameObjectRuntimeSet coinSet;
    public GameObjectRuntimeSet enemySet;

    public TMPro.TextMeshProUGUI coinCountTxt;
    public TMPro.TextMeshProUGUI centralTxt;

    [SerializeField] int coinCount = 0;

    int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        coinCountTxt.text = "Coin Count: 0";
        centralTxt.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Check for collisions
    private void OnTriggerEnter(Collider other)
    {
        //Coin pickup
        if(other.gameObject.tag == "Coin")
        {
            Debug.Log("Found a coin");
            coinCount++;
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
                centralTxt.text = "You died";
                centralTxt.enabled = true;
                StartCoroutine(OnPlayerLoss());
            }
        }
    }

    IEnumerator OnPlayerLoss()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
