using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

//Check if we've seen the player
public class CheckIfPlayerSeen : BTCondition
{
    Player player;
    NavMeshAgent agent;
    Vector3 playerPos;
    float distance;
    public CheckIfPlayerSeen(NavMeshAgent checkingAgent, Player player)
    {
        agent = checkingAgent;
        this.player = player;
    }

    protected override NodeState OnRun()
    {
        if (agent.GetComponent<Enemy>().sawPlayer)
        {
            Debug.Log("Spotted player");
            return NodeState.SUCCESS;
        }
        else if (!agent.GetComponent<Enemy>().sawPlayer)
        {
            Debug.Log("Doesn't see player");
            return NodeState.FAILURE;
        }
        return NodeState.FAILURE;
    }

    protected override void OnReset() { }
}
