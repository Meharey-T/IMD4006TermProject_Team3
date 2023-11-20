using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckIfPlayerAudible : BTCondition
{
    Enemy thisActor;
    public CheckIfPlayerAudible(Enemy enemy)
    {
        thisActor = enemy;
    }

    protected override NodeState OnRun()
    {
        if (thisActor.hearsPlayer || thisActor.heardPlayer)
        {
            Debug.Log("Heard player");
            return NodeState.SUCCESS;
        }
        else if (!thisActor.hearsPlayer && !thisActor.heardPlayer)
        {
            Debug.Log("Can't hear player");
            return NodeState.FAILURE;
        }
        return NodeState.FAILURE;
    }

    protected override void OnReset() { }
}
