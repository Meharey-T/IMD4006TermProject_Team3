using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckIfIndifferent : BTCondition
{
    Enemy thisActor;
    public CheckIfIndifferent(Enemy enemy)
    {
        thisActor = enemy;
    }

    protected override NodeState OnRun()
    {
        if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
        {
            //Debug.Log("Returning indifferent, success");
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }

    protected override void OnReset() { }
}
