using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/*
 * When creating a new enemy for a scene please do the following:
 * Drag the player instance in the scene into the Player Obj slot on this script
 * That's it, it should run
 * Check this script again later as it may get more complicated
 */
public class Enemy : MonoBehaviour
{
    public GameObjectRuntimeSet coinSet;
    public GameObjectRuntimeSet playerSet;
    public NavMeshAgent enemyMeshAgent;
    public GameObject playerObj;
    public Vector3 startingPos;
    Collider[] rangeChecks;
    public float defaultSpeed = 3.5f;
    //public int currentSpeed;

    //patrolling 
    public List<GameObject> Waypoints;
    //public int wayPointIndex;

    //Related to detection/seeing
    public bool seesPlayer = false;
    public bool sawPlayer = false;
    public Vector3 lastLocationSeen;
    public float detectionRadius;
    [Range(0, 360)]
    public float viewAngle;

    //Related to detection/hearing
    public bool hearsPlayer = false;
    public bool heardPlayer = false;
    public Vector3 lastLocationHeard;

    public bool hasStopped = false;

    //Handles their anger level
    public enum AngerLevel{INDIFFERENT, IRRITATED, ANGRY, FURIOUS};
    public AngerLevel angerLevel;
    public int totalTreasure = 0;
    public int treasureLeft;
    public int i_angerLevel;

    [SerializeField] GameObject emojiTarget;
    [SerializeField] GameObject emojiPrefab;
    Sprite i_currentEmoji;
    [SerializeField] Sprite i_indifferent;
    [SerializeField] Sprite i_irritated;
    [SerializeField] Sprite i_angry;
    [SerializeField] Sprite i_furious;

    public Transform camera;

    WaitForSeconds wait = new WaitForSeconds(0.2f);
    WaitForSeconds grabTime = new WaitForSeconds(2f);

    public bool playerInGrabRange;
    public bool caughtPlayer;


    // Start is called before the first frame update
    void Start()
    {
        enemyMeshAgent = GetComponent<NavMeshAgent>();
        playerObj = playerSet.Items[0].gameObject;
        startingPos = transform.position;
        CalculateTreasureOwned();
        rangeChecks = new Collider[4];
        StartCoroutine(FOVRoutine());
        StartCoroutine(AngerCheck());
        i_currentEmoji = i_indifferent;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        emojiTarget.transform.LookAt(camera.position + camera.forward);
    }

    //Reduces call count as this kind of behaviour can be a little computationally expensive
    private IEnumerator FOVRoutine()
    {
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
        rangeChecks = Physics.OverlapSphere(transform.position, detectionRadius, LayerMask.GetMask("Player"));
        //If the length is more than 0, we got something
        if (rangeChecks.Length != 0)
        {
            //
            Transform target = rangeChecks[0].transform;
            Vector3 facePosition = new Vector3(transform.position.x, transform.position.y + 3.25f, transform.position.z + 0.3f);
            //Vector3 facePosition = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            Vector3 directionToTarget = (target.position - facePosition).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //Physics.Raycast(transform.position, directionToTarget, distanceToTarget, LayerMask.GetMask("Ground"));
                if (!Physics.Raycast(facePosition, directionToTarget, distanceToTarget, LayerMask.GetMask("Ground")))
                {
                    seesPlayer = true;
                    lastLocationSeen = playerObj.transform.position;
                }
                else if (seesPlayer == true)
                {
                    seesPlayer = false;
                    sawPlayer = true;
                }
                else
                {
                    seesPlayer = false;
                }
            }
            else if (seesPlayer == true)
            {
                seesPlayer = false;
                sawPlayer = true;
            }
            else seesPlayer = false;
            rangeChecks[0] = null;
        }
        else if (seesPlayer)
        {
            seesPlayer = false;
            sawPlayer = true;
            lastLocationSeen = playerObj.transform.position;
        }
        
    }

    private void CalculateTreasureOwned()
    {
        for(int i = 0; i < coinSet.Items.Count; i++)
        {
            //Go through all treasure and then subtract by final chest amount since it's not going to affect gameplay
            totalTreasure += coinSet.Items[i].GetComponent<Treasure>().treasureStats.coinValue;
        }
        totalTreasure -= 25;
        treasureLeft = totalTreasure;
    }

    private IEnumerator AngerCheck()
    {
        while (true)
        {
            yield return wait;
            ResolveAngerLevel();
        }
    }

    //Sets the anger state of the enemy based on how much treasure is left
    private void ResolveAngerLevel()
    {
        //If the treasure left is greater than 75% of the total treasure
        if(treasureLeft > (totalTreasure / 4) * 3)
        {
            angerLevel = AngerLevel.INDIFFERENT;
            i_currentEmoji = i_indifferent;
            //enemyMeshAgent.speed = 3;
        }
        //If the treasure left is less than 75% but more than 50%
        else if (treasureLeft < (totalTreasure / 4) * 3 && treasureLeft > totalTreasure / 2)
        {
            angerLevel = AngerLevel.IRRITATED;
            i_currentEmoji = i_irritated;
            //enemyMeshAgent.speed = 3.5f;
        }
        //If the treasure left is less than 50% but more than 25%
        else if(treasureLeft < totalTreasure / 2 && treasureLeft > totalTreasure / 4)
        {
            angerLevel = AngerLevel.ANGRY;
            i_currentEmoji = i_angry;
            //enemyMeshAgent.speed = 4;
        }
        //If the treasure left is less than 25%
        else if(treasureLeft < totalTreasure / 4)
        {
            angerLevel = AngerLevel.FURIOUS;
            i_currentEmoji = i_furious;
            //enemyMeshAgent.speed = 4.5f;
        }
        emojiPrefab.GetComponentInChildren<Image>().sprite = i_currentEmoji;
    }

    public IEnumerator GrabPlayer()
    {
        Debug.Log("Grabbing player");
        yield return grabTime;
        if (playerInGrabRange)
        {
            Debug.Log("Finishing grabbing player");
            playerObj.GetComponent<Player>().OnPlayerLoseLife();
        }
        StopCoroutine(GrabPlayer());
        playerInGrabRange = false;
    }

     private void OnTriggerEnter(Collider other)
    {
        //Handles enemies running into traps
        if(other.gameObject.tag == "Trap")
        {
            this.GetComponent<Interactable>().Die();
        }

        //Handles if they fall into the radius of the player projected sound
        if (other.gameObject.layer == 9 && other.GetType() == typeof(SphereCollider) && !caughtPlayer)
        {
            //Sets them to alert and sets a value to their location heard, allowing them to pursue the source of the sound
            hearsPlayer = true;
            heardPlayer = false;
            lastLocationHeard = other.transform.position;
        }

        //Handles if they run into the player while they're not hiding
        //Only collides with the actual player geometry
        if (other.gameObject.layer == 9 && other.GetType() == typeof(BoxCollider))
        {
            //Player will lose a life
            Debug.Log("Player losing life the old fashioned way");
            other.GetComponentInParent<Player>().OnPlayerLoseLife();
        }
      }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 && other.GetType() == typeof(SphereCollider) && !caughtPlayer)
        {
            //Sets them to alert and sets a value to their location heard, allowing them to pursue the source of the sound
            hearsPlayer = true;
            heardPlayer = false;
            lastLocationHeard = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If they exit the range of where they can currently hear the player, sets it accordingly
        if (other.gameObject.layer == 9 && other.GetType() == typeof(SphereCollider) && !caughtPlayer)
        {
            //They can no longer hear the player, and the place they last heard them stops changing
            hearsPlayer = false;
            heardPlayer = true;
            lastLocationHeard = other.transform.position;
        }
    }
}
