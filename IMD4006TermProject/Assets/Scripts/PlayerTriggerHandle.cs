using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerHandle : MonoBehaviour
{
    private Player player;
    private PlayerMovement playerMovement;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    //Check for collisions
    private void OnTriggerEnter(Collider other)
    {
        //Coin pickup
        if (other.gameObject.tag == "Treasure")
        {
            Debug.Log("Found treasure");
            //Figure out what kind of treasure we just found, act accordingly
            player.coinCount += other.GetComponent<Treasure>().treasureStats.coinValue;
            if (other.GetComponent<Treasure>().treasureStats.finalChest)
            {
                Debug.Log("Player has won");
                StartCoroutine(player.OnPlayerWon());
            }
            //Remove the coin from the scene
            other.GetComponent<Interactable>().Die();
            player.coinCountTxt.text = "Coin Count: " + player.coinCount;
        }

        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("Hit an obstacle");
            player.OnPlayerLoseLife();
        }

        //Layer 7 is ground
        //Only the capsule collider should be able to trigger on a ground object
        if (other.gameObject.layer == 7)
        {
            playerMovement.groundedPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Layer 7 is ground
        if (other.gameObject.layer == 7)
        {
            playerMovement.groundedPlayer = false;
        }
    }
}
