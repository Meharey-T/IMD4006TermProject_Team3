using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class TaskCheckOutSound : BTNode
{
    NavMeshAgent agent;
    Enemy thisActor;

    public TaskCheckOutSound(Enemy enemy, NavMeshAgent enemyMeshAgent)
    {
        thisActor = enemy;
        agent = enemyMeshAgent;
    }

    protected override NodeState OnRun()
    {
        Debug.Log("Running TaskCheckOutSound");
        float waypointDistance = Vector3.Distance(thisActor.transform.position, thisActor.lastLocationHeard);

        //If we see the player, don't worry about what we heard, chase them
        if (agent.GetComponent<Enemy>().seesPlayer)
        {
            state = NodeState.FAILURE;
        }
        //If they reach the waypoint and don't see anything, go back to regular behaviours
        else if (waypointDistance < 2.5f)
        {
            state = NodeState.SUCCESS;
            thisActor.heardPlayer = false;
        }
        //If they haven't reached it, keep going
        else if (waypointDistance >= 2.5f)
        {
            agent.SetDestination(thisActor.lastLocationHeard);
            state = NodeState.RUNNING;
        }
        return state;
    }

    protected override void OnReset() { }
}
