using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

//Check if we've seen the player
public class CheckIfPlayerSeen : BTCondition
{
    NavMeshAgent agent;
    public CheckIfPlayerSeen(NavMeshAgent checkingAgent)
    {
        agent = checkingAgent;
    }

    protected override NodeState OnRun()
    {
        if (agent.GetComponent<Enemy>().sawPlayer)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }

    protected override void OnReset() { }
}
