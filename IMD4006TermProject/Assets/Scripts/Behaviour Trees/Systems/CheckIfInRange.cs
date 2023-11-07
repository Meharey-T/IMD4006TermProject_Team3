using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class CheckIfInRange : BTCondition
{
    NavMeshAgent agent;
    Vector3 playerPos;
    float distance;
    public CheckIfInRange(NavMeshAgent checkingAgent, Player player)
    {
        agent = checkingAgent;
        playerPos = player.transform.position;
        distance = Vector3.Distance(agent.transform.position, playerPos);
    }

    protected override NodeState OnRun()
    {
        if (distance <= 4f)
        {
            return NodeState.SUCCESS;
        }
        else if (distance > 4f)
        {
            return NodeState.FAILURE;
        }
        return NodeState.FAILURE;
    }

    protected override void OnReset() { }
}
