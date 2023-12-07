using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerHandle : MonoBehaviour
{
    private Player player;
    private PlayerMovement playerMovement;
    private TerrainState terrain;
    public Material[] materials;
    private void Start()
    {
        player = GetComponentInParent<Player>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        terrain = GetComponentInParent<TerrainState>();
    }

    //Check for collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            //Debug.Log("Hit an obstacle");
            player.OnPlayerLoseLife();
        }
        //Layer 7 is ground
       
    }

    private void OnTriggerStay(Collider other)
    {
        //Layer 7 is ground
        //Only the capsule collider should be able to trigger on a ground object
        if (other.gameObject.layer == 11)
        {
            terrain.terrainTag = other.gameObject.tag;
            Debug.Log("When checking this floor tag is:");
            Debug.Log(other.gameObject.tag);
   
        }
        if (other.gameObject.layer == 7 && other.gameObject.tag=="Crate")
        {
            terrain.terrainTag = other.gameObject.tag;
            Debug.Log("When checking this floor tag is:");
            Debug.Log(other.gameObject.tag);
        }
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
