using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class TaskCheckLastPlaceSeen : BTNode
{
    NavMeshAgent agent;
    Enemy thisActor;

    public TaskCheckLastPlaceSeen(Enemy enemy, NavMeshAgent enemyMeshAgent)
    {
        thisActor = enemy;
        agent = enemyMeshAgent;
    }

    protected override NodeState OnRun()
    {
        Debug.Log("Running TaskCheckLastPlaceSeen");
        float waypointDistance = Vector3.Distance(thisActor.transform.position, thisActor.lastLocationSeen);

        //Quickly abort this script if they hear or see the player; we want them to jump to the appropriate behaviours
        if (agent.GetComponent<Enemy>().seesPlayer || agent.GetComponent<Enemy>().hearsPlayer)
        {
            state = NodeState.FAILURE;
        }
        else if (waypointDistance >= 4f)
        {
            agent.SetDestination(thisActor.lastLocationSeen);
            state = NodeState.RUNNING;
        }
        //If they arrive at the destination we want them to stop, go back to regular behaviours
        else if (waypointDistance < 4f)
        {
            state = NodeState.SUCCESS;
            thisActor.sawPlayer = false;
        }
        //If they haven't reached the waypoint yet, keep running
        
        return state;
    }

    protected override void OnReset() { }
}
