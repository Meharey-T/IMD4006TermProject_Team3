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
