using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHandler : MonoBehaviour
{
    Enemy enemy;
    GameObject player;

    private void Start()
    {
        enemy = this.GetComponentInParent<Enemy>();
        player = enemy.playerObj;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && other.GetType() == typeof(BoxCollider))
        {
            //Starting grab animation goes here?
            enemy.playerInGrabRange = true;
            StartCoroutine(enemy.GrabPlayer());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && other.GetType() == typeof(BoxCollider))
        {
            enemy.playerInGrabRange = false;
        }
    }
}
