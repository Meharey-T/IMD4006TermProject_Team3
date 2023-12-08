using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class TaskCheckOutSound : BTNode
{
    NavMeshAgent agent;
    Enemy thisActor;
    Quaternion lookRotation;

    public TaskCheckOutSound(Enemy enemy, NavMeshAgent enemyMeshAgent)
    {
        thisActor = enemy;
        agent = enemyMeshAgent;
    }

    protected override NodeState OnRun()
    {
        //Debug.Log("Running TaskCheckOutSound");
        float waypointDistance = Vector3.Distance(thisActor.transform.position, thisActor.lastLocationHeard);
        agent.GetComponent<Enemy>().enemyAnimator.animator.SetBool(agent.GetComponent<Enemy>().enemyAnimator.IfTurningRightHash, false);
        agent.GetComponent<Enemy>().enemyAnimator.animator.SetBool(agent.GetComponent<Enemy>().enemyAnimator.IfTurningLeftHash, false);
        //agent.GetComponent<Enemy>().enemyAnimator.animator.SetBool(agent.GetComponent<Enemy>().enemyAnimator.IfGrabbingHash, false);

        if (agent.speed == thisActor.defaultSpeed)
        {
            //agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetHearState();
            if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
            {
                agent.speed = 3.5f;
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, true);
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, false);
            }
            else if (thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
            {
                agent.speed = 4f;

                //set the sound effect to irritated

                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, true);
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, false);
                //set the sound effect to indiffernt

                Debug.Log("I hear you SFX is set irritated");
                agent.GetComponent<EnemySoundFX>().currEmotion = EnemySoundFX.AngerLevel.IRRITATED;
                // check if had said something before
                agent.GetComponent<EnemySoundFX>().hasHearSoundSaid();
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
        }

        //If we see the player, don't worry about what we heard, chase them
        if (agent.GetComponent<Enemy>().seesPlayer)
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
            //Debug.Log("Checking out sound");
        }
        return state;
    }

    protected override void OnReset() { }
}
