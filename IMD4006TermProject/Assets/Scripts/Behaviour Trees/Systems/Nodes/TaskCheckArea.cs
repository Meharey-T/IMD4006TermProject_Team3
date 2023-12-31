using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskCheckArea : BTNode
{
    Transform BTTransform;
    Enemy thisActor;
    int waypointRadius = 1;
    bool setWayPoint = false;
    Quaternion lookRotation;

    public TaskCheckArea(Transform transform)
    {
        BTTransform = transform;
        thisActor = transform.GetComponent<Enemy>();
    }

    protected override NodeState OnRun()
    {
        thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, false);
        thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, false);
        //thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfGrabbingHash, false);
        //If we see the player, we stop doing this right away
        if (BTTransform.GetComponent<Enemy>().seesPlayer)
        {
            state = NodeState.FAILURE;
        }
        else if (thisActor.caughtPlayer)
        {
            state = NodeState.FAILURE;
        }
        //Otherwise we need to set up an initial turn direction
        else if (!setWayPoint)
        {
            Vector3 turnToPoint = CreateTurnToPoint();
            Vector3 direction = turnToPoint.normalized;
            if (direction.x < 0)
            {
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningLeftHash, true);
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningRightHash, false);
            }
            else if (direction.x > 0)
            {
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningRightHash, true);
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningLeftHash, false);
            }
            else if (direction.x == 0)
            {
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningLeftHash, false);
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningRightHash, false);
            }
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            state = NodeState.RUNNING;
            setWayPoint = true;
        }
        //If we've already set that, then we turn to it
        else if (setWayPoint){
            BTTransform.rotation = Quaternion.Slerp(BTTransform.rotation, lookRotation, Time.deltaTime * 5f);
            state = NodeState.RUNNING;
        }
        return state;
    }

    private Vector3 CreateTurnToPoint()
    {
        float waypointZ = Random.Range(-waypointRadius, waypointRadius);
        float waypointX = Random.Range(-waypointRadius, waypointRadius);
        Vector3 proposedWaypoint = new Vector3(waypointX + BTTransform.position.x, this.BTTransform.position.y, waypointZ + BTTransform.position.z);
        return proposedWaypoint;
    }

    protected override void OnReset() { }
}
