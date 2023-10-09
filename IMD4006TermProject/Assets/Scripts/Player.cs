using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObjectRuntimeSet coinSet;
    public GameObjectRuntimeSet enemySet;

    public TMPro.TextMeshProUGUI coinCountTxt;

    [SerializeField] int coinCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        coinCountTxt.text = "Coin Count: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            Debug.Log("Found a coin");
            coinCount++;
            other.GetComponent<Interactable>().Die();
            coinCountTxt.text = "Coin Count: " + coinCount;
        }
    }
}
