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

        if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
        {
            //Debug.Log(thisActor.angerLevel);
            agent.speed = 3.5f;
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, true);
        }
        else if (thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
        {
            agent.speed = 4f;
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, true);
        }
        else if (thisActor.angerLevel == Enemy.AngerLevel.ANGRY)
        {
            agent.speed = 5f;
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, true);
        }
        else if (thisActor.angerLevel == Enemy.AngerLevel.FURIOUS)
        {
            agent.speed = 6f;
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, true);
        }

        //Debug.Log("What in the world is happening???");

        //Quickly abort this script if they hear or see the player; we want them to jump to the appropriate behaviours
        if (thisActor.seesPlayer || thisActor.hearsPlayer)
        {
            //Debug.Log("Detected player, aborting CheckLastPlaceSeen");
            state = NodeState.FAILURE;
        }
        //If they haven't arrived at their destination yet, keep running
        else if (waypointDistance >= 4f)
        {
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
            //thisActor.sawPlayer = false;
        }
        return state;
    }

    protected override void OnReset() { }
}
