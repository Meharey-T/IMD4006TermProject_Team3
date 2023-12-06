using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskChillASecond : BTNode
{
    NavMeshAgent agent;
    //We may want to put various idle animations in this, we can pass them in for various different idles to get different results
    //Then we can reuse this for various tasks like chopping up our food or whatever else they may want to do while standing around
    public TaskChillASecond(NavMeshAgent enemyAgent)
    {
        agent = enemyAgent;
    }

    protected override NodeState OnRun()
    {
        if (agent.GetComponent<Enemy>().seesPlayer || agent.GetComponent<Enemy>().hearsPlayer
            || agent.GetComponent<Enemy>().sawPlayer || agent.GetComponent<Enemy>().heardPlayer)
        {
            state = NodeState.FAILURE;
        }
        else
        {
            state = NodeState.RUNNING;
        }
        return state;
    }

    protected override void OnReset() { }
}
