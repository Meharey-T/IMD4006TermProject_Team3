using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class CheckIfVisible : BTCondition
{
    NavMeshAgent agent;

    public CheckIfVisible(NavMeshAgent checkingAgent)
    {
        agent = checkingAgent;
    }

    protected override NodeState OnRun()
    {
        if (agent.GetComponent<Enemy>().seesPlayer)
        {
            //Debug.Log("Spotted player");
            return NodeState.SUCCESS;
        }
        else
        {
            //Debug.Log("Doesn't see player");
            return NodeState.FAILURE;
        }
    }

    protected override void OnReset() { }
}
