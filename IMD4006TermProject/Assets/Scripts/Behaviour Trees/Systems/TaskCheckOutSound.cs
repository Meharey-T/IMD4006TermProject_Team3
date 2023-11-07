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
        float waypointDistance = Vector3.Distance(thisActor.transform.position, thisActor.lastLocationHeard);


        if (waypointDistance < 1)
        {
            state = NodeState.SUCCESS;
            thisActor.hearsPlayer = false;
            //NewPatrolPoint();
        }
        else if (waypointDistance >= 1)
        {
            agent.SetDestination(thisActor.lastLocationHeard);
            state = NodeState.RUNNING;
        }
        return state;
    }

    protected override void OnReset() { }
}
