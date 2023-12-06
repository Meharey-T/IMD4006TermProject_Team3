using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

//This behaviour node has the character actually chase down the player
public class TaskChasePlayer : BTNode
{
    Transform transform;
    NavMeshAgent agent;
    Player player;
    Enemy thisActor;

    public TaskChasePlayer(Transform transform, NavMeshAgent enemyMeshAgent, Player player)
    {
        this.transform = transform;
        agent = enemyMeshAgent;
        this.player = player;
        thisActor = agent.GetComponent<Enemy>();
    }

    protected override NodeState OnRun()
    {
        //Debug.Log("Running TaskChasePlayer");
        float waypointDistance = Vector3.Distance(transform.position, player.transform.position);
        if (agent.speed == thisActor.defaultSpeed)
        {
            if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
            {
                agent.speed = 3.5f;
            }
            else if (thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
            {
                agent.speed = 4f;
            }
            else if (thisActor.angerLevel == Enemy.AngerLevel.ANGRY)
            {
                agent.speed = 5f;
            }
            else if (thisActor.angerLevel == Enemy.AngerLevel.FURIOUS)
            {
                agent.speed = 6f;
            }
        }

        //Debug.Log("This character sees player: " + thisActor.seesPlayer);

        //If we're basically on the player now
        if (waypointDistance < 1)
        {
            agent.speed = thisActor.defaultSpeed;
            state = NodeState.SUCCESS;
        }
        //If at any point the enemy can no longer see the player, this function fails and we change behaviour
        else if(!thisActor.seesPlayer)
        {
            thisActor.sawPlayer = true;
            agent.speed = thisActor.defaultSpeed;
            state = NodeState.FAILURE;
        }
        //If we can still see the player but haven't reached them yet
        else if (waypointDistance >= 1 && thisActor.seesPlayer)
        {
            //Check if the player is standing on something above the ground
            if(player.transform.position.y > 0.3 && player.GetComponent<PlayerMovement>().groundedPlayer)
            {
                Debug.Log("Attempting to find the closest path to the player");
                Vector3 closestNavPoint = FindNearestPathable();
                agent.SetDestination(closestNavPoint);
            }
            else
            { 
                agent.SetDestination(player.transform.position);
            }
           
            // Debug.Log("going to new position");
            state = NodeState.RUNNING;
        }
        return state;
    }

    private Vector3 FindNearestPathable()
    {
        //Try to find a point near the player on the ground
        Vector3 testPoint = new Vector3(player.transform.position.x, 0, player.transform.position.z) + Random.insideUnitSphere * 1.5f;
        while (!TestPoint(testPoint))
        {
            testPoint = new Vector3(player.transform.position.x, 0, player.transform.position.z) + Random.insideUnitSphere * 1.5f;
        }
        return testPoint;
    }
    private bool TestPoint(Vector3 proposedWaypoint)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(proposedWaypoint, out hit, 1f, NavMesh.AllAreas);
    }

    protected override void OnReset() { }
}
