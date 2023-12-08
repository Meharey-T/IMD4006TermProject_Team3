using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckIfPlayerHeard : BTCondition
{
    Enemy thisActor;
    public CheckIfPlayerHeard(Enemy enemy)
    {
        thisActor = enemy;
    }

    protected override NodeState OnRun()
    {
        if (thisActor.heardPlayer)
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
