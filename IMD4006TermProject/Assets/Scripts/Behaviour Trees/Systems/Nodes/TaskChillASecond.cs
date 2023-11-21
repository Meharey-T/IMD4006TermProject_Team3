using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskChillASecond : BTNode
{
    //We may want to put various idle animations in this, we can pass them in for various different idles to get different results
    //Then we can reuse this for various tasks like chopping up our food or whatever else they may want to do while standing around
    public TaskChillASecond()
    {
        
    }

    protected override NodeState OnRun()
    {
        state = NodeState.SUCCESS;
        return state;
    }

    protected override void OnReset() { }
}
