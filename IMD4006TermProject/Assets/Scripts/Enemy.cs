using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemyMeshAgent;
    public GameObject playerObj;
    Vector3 enemyFollowTarget;

    //patrolling 
    public List<GameObject> Waypoints;
    float waypointRadius;
    Vector3 nextWaypointPos;

    //Related to detection/seeing
    public bool seesPlayer = false;
    public Vector3 lastLocationSeen;
    public float detectionRadius;
    [Range(0, 360)]
    public float viewAngle;

    //Related to detection/hearing
    public bool hearsPlayer = false;
    public Vector3 lastLocationHeard;


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
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    //From tutorial https://www.youtube.com/watch?v=j1-OyLo77ss
    private void FieldOfViewCheck()
    {
        //Physics.OverlapSphere gets all of the colliders around the source in a certain radius, and looks for specific collision layers
        //We're using it to check for players in a certain range
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, detectionRadius, LayerMask.GetMask("Player"));

        //If the length is more than 0, we got something
        if(rangeChecks.Length != 0)
        {
            //
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, LayerMask.GetMask("Ground")))
                {
                    seesPlayer = true;
                }
                else seesPlayer = false;
            }
            else seesPlayer = false;
        }
        else if (seesPlayer)
        {
            seesPlayer = false;
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        //Handles enemies running into traps
        if(other.gameObject.tag == "Trap")
        {
         this.GetComponent<Interactable>().Die();
        }

        //Handles if they run into the player while they're not hiding
        //Only collides with the actual player geometry
        if(other.gameObject.layer == 9 && other.GetType() == typeof(BoxCollider))
        {
            //Player will lose a life
            other.GetComponentInParent<Player>().OnPlayerLoseLife();
        }
        //Handles if they fall into the radius of the player projected sound
        if(other.gameObject.layer == 9 && other.GetType() == typeof(SphereCollider))
        {
            //Sets them to alert and sets a value to their location heard, allowing them to pursue the source of the sound
            hearsPlayer = true;
            lastLocationHeard = other.transform.position;
        }
      }

    private void OnTriggerExit(Collider other)
    {
        //If they exit the range of where they can currently hear the player, sets it accordingly
        if (other.gameObject.tag == "player" && other.GetType() == typeof(SphereCollider))
        {
            //They can no longer hear the player, and the place they last heard them stops changing
            hearsPlayer = false;
            lastLocationHeard = other.transform.position;
        }
    }


}
