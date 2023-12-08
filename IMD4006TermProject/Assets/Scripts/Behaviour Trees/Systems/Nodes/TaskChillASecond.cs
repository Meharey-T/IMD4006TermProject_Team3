using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskChillASecond : BTNode
{
    NavMeshAgent agent;
    Enemy thisActor;
    //We may want to put various idle animations in this, we can pass them in for various different idles to get different results
    //Then we can reuse this for various tasks like chopping up our food or whatever else they may want to do while standing around
    public TaskChillASecond(NavMeshAgent enemyAgent)
    {
        agent = enemyAgent;
        thisActor = agent.GetComponent<Enemy>();
    }

    protected override NodeState OnRun()
    {
        if (thisActor.seesPlayer || thisActor.hearsPlayer
            || thisActor.sawPlayer || thisActor.heardPlayer)
        {
            state = NodeState.FAILURE;
        }
        else if (thisActor.caughtPlayer)
        {
            state = NodeState.FAILURE;
        }
        else
        {
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, false);
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, false);
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningLeftHash, false);
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningRightHash, false);
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfGrabbingHash, false);
            state = NodeState.RUNNING;
        }
        return state;
    }

    protected override void OnReset() { }
}
