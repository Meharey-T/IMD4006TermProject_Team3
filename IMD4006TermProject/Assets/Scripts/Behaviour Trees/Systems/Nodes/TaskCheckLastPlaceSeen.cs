using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class TaskCheckLastPlaceSeen : BTNode
{
    NavMeshAgent agent;
    Enemy thisActor;

    public TaskCheckLastPlaceSeen(Enemy enemy, NavMeshAgent enemyMeshAgent)
    {
        thisActor = enemy;
        agent = enemyMeshAgent;
    }

    protected override NodeState OnRun()
    {
        Debug.Log("Running TaskCheckLastPlaceSeen");
        float waypointDistance = Vector3.Distance(thisActor.transform.position, thisActor.lastLocationSeen);

        if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
        {
                Debug.Log(thisActor.angerLevel);
                agent.speed = 3.5f;
        }
        else if (thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
        {
            agent.speed = 4f;
        }
        else if (thisActor.angerLevel == Enemy.AngerLevel.ANGRY)
        {
            agent.speed = 5f;
        }
        else if (thisActor.angerLevel == Enemy.AngerLevel.FURIOUS)
        {
            agent.speed = 6f;
        }

        //Debug.Log("What in the world is happening???");

        //Quickly abort this script if they hear or see the player; we want them to jump to the appropriate behaviours
        if (thisActor.seesPlayer || thisActor.hearsPlayer)
        {
            Debug.Log("Detected player, aborting CheckLastPlaceSeen");
            state = NodeState.FAILURE;
        }
        //If they haven't arrived at their destination yet, keep running
        else if (waypointDistance >= 4f)
        {
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
