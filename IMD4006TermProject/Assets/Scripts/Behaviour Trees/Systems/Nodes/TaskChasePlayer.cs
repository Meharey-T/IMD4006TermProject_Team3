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

    public TaskChasePlayer(Transform transform, NavMeshAgent enemyMeshAgent, Player player)
    {
        this.transform = transform;
        agent = enemyMeshAgent;
        this.player = player;
    }

    protected override NodeState OnRun()
    {
        float waypointDistance = Vector3.Distance(transform.position, player.transform.position);

        //If we're basically on the player now
        if (waypointDistance < 1)
        {
            state = NodeState.SUCCESS;
        }
        //If at any point the enemy can no longer see the player, this function fails and we change behaviour
        else if(!transform.GetComponent<Enemy>().seesPlayer)
        {
            transform.GetComponent<Enemy>().sawPlayer = true;
            state = NodeState.FAILURE;
        }
        //If we can still see the player but haven't reached them yet
        else if (waypointDistance >= 1 && transform.GetComponent<Enemy>().seesPlayer)
        {
            agent.SetDestination(player.transform.position);
            // Debug.Log("going to new position");
            state = NodeState.RUNNING;
        }
        return state;
    }

    protected override void OnReset() { }
}
