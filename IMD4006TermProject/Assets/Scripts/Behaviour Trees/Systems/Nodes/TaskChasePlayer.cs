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
        Debug.Log("Running TaskChasePlayer");
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
            agent.SetDestination(player.transform.position);
            // Debug.Log("going to new position");
            state = NodeState.RUNNING;
        }
        return state;
    }

    protected override void OnReset() { }
}
