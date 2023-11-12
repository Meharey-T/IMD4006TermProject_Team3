using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

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


        if (waypointDistance < 1)
        {
            state = NodeState.SUCCESS;
            //NewPatrolPoint();
        }
        else if(!transform.GetComponent<Enemy>().seesPlayer)
        {
            state = NodeState.FAILURE;
        }
        else if (waypointDistance >= 1 && transform.GetComponent<Enemy>().seesPlayer)
        {
            agent.SetDestination(player.transform.position);
            //NewPatrolPoint();
            // Debug.Log("going to new position");
            state = NodeState.RUNNING;
        }
        return state;
    }

    protected override void OnReset() { }
}
