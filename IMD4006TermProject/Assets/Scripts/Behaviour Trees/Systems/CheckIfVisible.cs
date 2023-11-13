using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class CheckIfVisible : BTCondition
{
    Player player;
    NavMeshAgent agent;
    Vector3 playerPos;
    float distance;
    public CheckIfVisible(NavMeshAgent checkingAgent, Player player)
    {
        agent = checkingAgent;
        this.player = player;
    }

    protected override NodeState OnRun()
    {
        //playerPos = player.transform.position;
        //distance = Vector3.Distance(agent.transform.position, playerPos);
        if (agent.GetComponent<Enemy>().seesPlayer)
        {
            Debug.Log("Spotted player");
            return NodeState.SUCCESS;
        }
        else if (!agent.GetComponent<Enemy>().seesPlayer)
        {
            Debug.Log("Doesn't see player");
            return NodeState.FAILURE;
        }
        return NodeState.FAILURE;
    }

    protected override void OnReset() { }
}