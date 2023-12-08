using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class TaskCheckLastPlaceSeen : BTNode
{
    NavMeshAgent agent;
    Enemy thisActor;
    Quaternion lookRotation;

    public TaskCheckLastPlaceSeen(Enemy enemy, NavMeshAgent enemyMeshAgent)
    {
        thisActor = enemy;
        agent = enemyMeshAgent;
    }

    protected override NodeState OnRun()
    {
        //Debug.Log("Running TaskCheckLastPlaceSeen");
        float waypointDistance = Vector3.Distance(thisActor.transform.position, thisActor.lastLocationSeen);
        thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningRightHash, false);
        thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningLeftHash, false);
        //thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfGrabbingHash, false);

        if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
        {
            agent.speed = 3.5f;
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, true);
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, false);
        }
        else if (thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
        {
            agent.speed = 4f;
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, true);
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, false);
        }
        else if (thisActor.angerLevel == Enemy.AngerLevel.ANGRY)
        {
            agent.speed = 5f;
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, true);
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, false);
        }
        else if (thisActor.angerLevel == Enemy.AngerLevel.FURIOUS)
        {
            agent.speed = 6f;
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, true);
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, false);
        }

        //Debug.Log("What in the world is happening???");

        //Quickly abort this script if they hear or see the player; we want them to jump to the appropriate behaviours
        if (thisActor.seesPlayer || thisActor.hearsPlayer)
        {
            agent.ResetPath();
            state = NodeState.FAILURE;
        }
        else if (thisActor.caughtPlayer)
        {
            agent.ResetPath();
            state = NodeState.FAILURE;
        }
        //If they haven't arrived at their destination yet, keep running
        else if (waypointDistance >= 4f)
        {
            if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT || thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
            {
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, true);
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, false);
            }
            else if (thisActor.angerLevel == Enemy.AngerLevel.ANGRY || thisActor.angerLevel == Enemy.AngerLevel.FURIOUS)
            {
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, true);
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, false);
            }
            Vector3 direction = (thisActor.lastLocationSeen - thisActor.transform.position).normalized;
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            thisActor.transform.rotation = Quaternion.Slerp(thisActor.transform.rotation, lookRotation, Time.deltaTime * 5f);
            agent.SetDestination(thisActor.lastLocationSeen);
            state = NodeState.RUNNING;
        }
        //If they arrive at the destination we want them to stop, go back to regular behaviours
        else if (waypointDistance < 4f)
        {
            state = NodeState.SUCCESS;
            agent.speed = thisActor.defaultSpeed;
        }
        return state;
    }

    protected override void OnReset() { }
}
