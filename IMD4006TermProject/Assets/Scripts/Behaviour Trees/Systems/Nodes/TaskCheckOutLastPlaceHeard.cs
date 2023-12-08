using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class TaskCheckOutLastPlaceHeard : BTNode
{
    NavMeshAgent agent;
    Enemy thisActor;
    Quaternion lookRotation;

    public TaskCheckOutLastPlaceHeard(Enemy enemy, NavMeshAgent enemyMeshAgent)
    {
        thisActor = enemy;
        agent = enemyMeshAgent;
    }

    protected override NodeState OnRun()
    {
        //Debug.Log("Running TaskCheckOutLastPlaceHeard");
        float waypointDistance = Vector3.Distance(thisActor.transform.position, thisActor.lastLocationHeard);
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

        //If we see the player, don't worry about what we heard, chase them
        if (agent.GetComponent<Enemy>().seesPlayer || agent.GetComponent<Enemy>().hearsPlayer ||
            agent.GetComponent<Enemy>().sawPlayer)
        {
            agent.ResetPath();
            state = NodeState.FAILURE;
        }
        else if (thisActor.caughtPlayer)
        {
            agent.ResetPath();
            state = NodeState.FAILURE;
        }
        //If they reach the waypoint and don't see anything, go back to regular behaviours
        else if (waypointDistance < 4f)
        {
            agent.speed = thisActor.defaultSpeed;
            state = NodeState.SUCCESS;
            //thisActor.heardPlayer = false;
        }
        //If they haven't reached it, keep going
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
            Vector3 direction = (thisActor.lastLocationHeard - thisActor.transform.position).normalized;
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            thisActor.transform.rotation = Quaternion.Slerp(thisActor.transform.rotation, lookRotation, Time.deltaTime * 5f);
            agent.SetDestination(thisActor.lastLocationHeard);
            state = NodeState.RUNNING;
        }
        return state;
    }

    protected override void OnReset() { }
}