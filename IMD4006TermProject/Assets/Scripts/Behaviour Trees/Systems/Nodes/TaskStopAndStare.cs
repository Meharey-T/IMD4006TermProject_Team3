using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskStopAndStare : BTNode
{
    Player player;
    Quaternion lookRotation;
    Enemy thisActor;

    public TaskStopAndStare(Player player, Enemy enemy)
    {
        this.player = player;
        thisActor = enemy;
    }

    protected override NodeState OnRun()
    {
        thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, false);
        thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, false);
        //Debug.Log("Running TaskStopAndStare");
        thisActor.enemyMeshAgent.ResetPath();
        Vector3 direction = (player.transform.position - thisActor.transform.position).normalized;
        if (direction.z < 0)
        {
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningLeftHash, true);
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningRightHash, false);
        }
        else if (direction.z > 0)
        {
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningRightHash, true);
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningLeftHash, false);
        }
        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        thisActor.transform.rotation = Quaternion.Slerp(thisActor.transform.rotation, lookRotation, Time.deltaTime * 5f);
        state = NodeState.RUNNING;
        return state;
    }

    protected override void OnReset() { }
}
