using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckIfCaughtPlayer : BTCondition
{
    Enemy thisActor;
    public CheckIfCaughtPlayer(Enemy enemy)
    {
        thisActor = enemy;
    }

    protected override NodeState OnRun()
    {
        //Debug.Log("This actor has stopped: " + thisActor.hasStopped);
        if (thisActor.caughtPlayer)
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
