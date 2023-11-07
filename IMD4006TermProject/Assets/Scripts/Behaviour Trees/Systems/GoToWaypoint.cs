using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToWaypoint : BTNode
{
    Transform BTTransform;
    Vector3 nextWaypointPos;
    NavMeshAgent navigator;
    float waypointRadius;

    public GoToWaypoint(Transform transform, float waypointRadius, NavMeshAgent enemyAgent)
    {
        this.waypointRadius = waypointRadius;
        NewPatrolPoint();
        BTTransform = transform;
        navigator = enemyAgent;
    }

    public GoToWaypoint(Transform transform, NavMeshAgent enemyAgent, Vector3 targetPos)
    {
        BTTransform = transform;
        navigator = enemyAgent;
        nextWaypointPos = targetPos;
    }

    protected override NodeState OnRun()
    {
        float waypointDistance = Vector3.Distance(BTTransform.position, nextWaypointPos);


        if (waypointDistance < 1)
        {
            Debug.Log("Reached waypoint");
            state = NodeState.SUCCESS;
            NewPatrolPoint();
        }
        else if (waypointDistance >= 1)
        {
            navigator.SetDestination(nextWaypointPos);
            //NewPatrolPoint();
            // Debug.Log("going to new position");
            state = NodeState.RUNNING;
        }
        return state;
    }

    private void NewPatrolPoint()
    {
        float waypointZ = Random.Range(-waypointRadius, waypointRadius);
        float waypointX = Random.Range(-waypointRadius, waypointRadius);
        nextWaypointPos.Set(waypointX, 1f, waypointZ);
    }

    protected override void OnReset() { }

}
