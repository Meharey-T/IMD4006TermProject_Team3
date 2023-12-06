using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskSetHasStoppedTrue : BTNode
{
    Enemy thisActor;

    public TaskSetHasStoppedTrue(Enemy enemy)
    {
        thisActor = enemy;
    }

    protected override NodeState OnRun()
    {
        thisActor.hasStopped = true;
        state = NodeState.SUCCESS;
        return state;
    }

    protected override void OnReset() { }
}
