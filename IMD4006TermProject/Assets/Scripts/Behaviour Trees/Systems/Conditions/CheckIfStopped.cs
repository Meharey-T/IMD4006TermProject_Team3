using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckIfStopped : BTCondition
{
    Enemy thisActor;
    public CheckIfStopped(Enemy enemy)
    {
        thisActor = enemy;
    }

    protected override NodeState OnRun()
    {
        //Debug.Log("This actor has stopped: " + thisActor.hasStopped);
        if (thisActor.hasStopped)
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