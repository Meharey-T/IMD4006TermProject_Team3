using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class TaskClearDetection : BTNode
{
    NavMeshAgent agent;
    Enemy thisActor;

    public TaskClearDetection(Enemy enemy, NavMeshAgent enemyMeshAgent)
    {
        thisActor = enemy;
        agent = enemyMeshAgent;
    }

    protected override NodeState OnRun()
    {
        Debug.Log("Running TaskClearDetection");
        thisActor.hearsPlayer = false;
        thisActor.seesPlayer = false;
        thisActor.sawPlayer = false;
        thisActor.heardPlayer = false;
        thisActor.hasStopped = false;
        state = NodeState.SUCCESS;

        return state;
    }

    protected override void OnReset() { }
}
