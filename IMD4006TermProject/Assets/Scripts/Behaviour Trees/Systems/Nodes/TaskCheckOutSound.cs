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

        if (agent.speed == thisActor.defaultSpeed)

        {
            //agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetHearState();
            if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
            {
                agent.speed = 3.5f;
                //set the sound effect to indiffernt
               // agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetIndifference();
             //  Debug.Log("I hear you SFX is set indifferent");
               // check if had said something before
              // agent.GetComponent<EnemySoundFX>().hasHearSoundSaid();

             
            }
            else if (thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
            {
                agent.speed = 4f;
                //set the sound effect to irritated
                Debug.Log("I hear you SFX is set irritated");
                agent.GetComponent<EnemySoundFX>().currEmotion = EnemySoundFX.AngerLevel.IRRITATED;
                // check if had said something before
                agent.GetComponent<EnemySoundFX>().hasHearSoundSaid();
            }
            else if (thisActor.angerLevel == Enemy.AngerLevel.ANGRY)
            {
                agent.speed = 5f;
            }
            else if (thisActor.angerLevel == Enemy.AngerLevel.FURIOUS)
            {
                agent.speed = 6f;
            }
        }
        //Debug.Log("What in the world is happening???");

        //If we see the player, don't worry about what we heard, chase them
        if (agent.GetComponent<Enemy>().seesPlayer)
        {
            //Debug.Log("Spotted player, aborting checkoutsound");
            state = NodeState.FAILURE;
        }
        //If they reach the waypoint and don't see anything, go back to regular behaviours
        else if (waypointDistance < 4f)
        {
            agent.speed = thisActor.defaultSpeed;
            state = NodeState.SUCCESS;
            //Debug.Log("Finished checking location");
        }
        //If they haven't reached it, keep going
        else if (waypointDistance >= 4f)
        {
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
