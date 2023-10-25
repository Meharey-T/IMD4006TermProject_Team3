using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemyMeshAgent;
    public GameObject playerObj;
    Vector3 enemyFollowTarget;

    //patroling 
    public Transform[] Waypoints;
    float waypointRadius;
    Vector3 nextWaypointPos;


    bool toFollow = false;


    // Start is called before the first frame update
    void Start()
    {


        waypointRadius = 5;
        enemyMeshAgent = GetComponent<NavMeshAgent>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("The player's position is " + playerObj.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(enemyMeshAgent.transform.position, playerObj.transform.position);
        if (distance <= 4f)
        {
            toFollow = true;
            Following();
        }
        else if (distance > 4f)
        {
            Patroling();
            toFollow = false;
        }

    }

    private void Patroling()
    {


        float waypointDistance = Vector3.Distance(enemyMeshAgent.transform.position, nextWaypointPos);


        if (waypointDistance < 1)
        {
            NewPatrolPoint();
            //Debug.Log("Finding new position");

        }
        else if (waypointDistance >= 1)
        {
            enemyMeshAgent.SetDestination(nextWaypointPos);
           // Debug.Log("going to new position");

        }



       // Debug.Log(nextWaypointPos);

       // Debug.Log("patroling");

    }



    private void NewPatrolPoint()
    {
        float waypointZ = Random.Range(-waypointRadius, waypointRadius);
        float waypointX = Random.Range(-waypointRadius, waypointRadius);
        nextWaypointPos.Set(waypointX, 1f, waypointZ);




    }
    private void Following()
    {
        enemyMeshAgent.SetDestination(playerObj.transform.position);
       // Debug.Log("chasing");

        //enemyFollowTarget = gameObject.transform.position + (playerObj.transform.position - gameObject.transform.position) * 0.001f;
        // gameObject.transform.position = enemyFollowTarget;

    }
     private void OnTriggerEnter(Collider other)
    {
        //Coin pickup
        if(other.gameObject.tag == "Trap")
        {
         this.GetComponent<Interactable>().Die();
        }
      }


}
