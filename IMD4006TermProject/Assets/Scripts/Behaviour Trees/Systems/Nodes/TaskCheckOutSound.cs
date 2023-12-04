using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class TaskCheckOutSound : BTNode
{
    NavMeshAgent agent;
    Enemy thisActor;

    public TaskCheckOutSound(Enemy enemy, NavMeshAgent enemyMeshAgent)
    {
        thisActor = enemy;
        agent = enemyMeshAgent;
    }

    protected override NodeState OnRun()
    {
        Debug.Log("Running TaskCheckOutSound");
        float waypointDistance = Vector3.Distance(thisActor.transform.position, thisActor.lastLocationHeard);

        if (agent.speed == thisActor.defaultSpeed)
        {
            if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
            {
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
        }

        //If we see the player, don't worry about what we heard, chase them
        if (agent.GetComponent<Enemy>().seesPlayer || agent.GetComponent<Enemy>().sawPlayer)
        {
            agent.speed = thisActor.defaultSpeed;
            state = NodeState.FAILURE;
        }
        //If they reach the waypoint and don't see anything, go back to regular behaviours
        else if (waypointDistance < 4f)
        {
            agent.speed = thisActor.defaultSpeed;
            state = NodeState.SUCCESS;
            thisActor.heardPlayer = false;
        }
        //If they haven't reached it, keep going
        else if (waypointDistance >= 4f)
        {
            agent.SetDestination(thisActor.lastLocationHeard);
            state = NodeState.RUNNING;
        }
        return state;
    }

    protected override void OnReset() { }
}
