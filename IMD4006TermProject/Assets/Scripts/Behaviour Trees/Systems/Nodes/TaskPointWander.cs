using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskPointWander : BTNode
{
    Transform BTTransform;
    Vector3 nextWaypointPos;
    NavMeshAgent navigator;
    float waypointRadius;
    GameObject waypoint;

    public TaskPointWander(Transform transform, GameObject waypoint,  float waypointRadius, NavMeshAgent enemyAgent)
    {
        this.waypointRadius = waypointRadius;
        NewPatrolPoint();
        BTTransform = transform;
        navigator = enemyAgent;
        this.waypoint = waypoint;
    }

    protected override NodeState OnRun()
    {
        Debug.Log("Running TaskPointWander");
        float waypointDistance = Vector3.Distance(BTTransform.position, nextWaypointPos);

        if (navigator.GetComponent<Enemy>().seesPlayer || navigator.GetComponent<Enemy>().hearsPlayer ||
            navigator.GetComponent<Enemy>().sawPlayer || navigator.GetComponent<Enemy>().heardPlayer)
        {
            state = NodeState.FAILURE;
        }
        else if (waypointDistance >= 1)
        {
            navigator.SetDestination(nextWaypointPos);
            state = NodeState.RUNNING;
        }
        else if (waypointDistance < 1)
        {
            Debug.Log("Reached waypoint");
            state = NodeState.SUCCESS;
            NewPatrolPoint();
        }
        
        return state;
    }

    private void NewPatrolPoint()
    {
        float waypointZ = Random.Range(-waypointRadius, waypointRadius);
        float waypointX = Random.Range(-waypointRadius, waypointRadius);
        Vector3 proposedWaypoint = new Vector3(waypointX + waypoint.transform.position.x, 1, waypointZ + waypoint.transform.position.z);

        while (!TestPoint(proposedWaypoint))
        {
            waypointZ = Random.Range(-waypointRadius, waypointRadius);
            waypointX = Random.Range(-waypointRadius, waypointRadius);
            proposedWaypoint = new Vector3(waypointX + waypoint.transform.position.x, 1, waypointZ + waypoint.transform.position.z);
        }
        nextWaypointPos.Set(waypointX + waypoint.transform.position.x, 1f, waypointZ + waypoint.transform.position.z);
    }

    private bool TestPoint(Vector3 proposedWaypoint)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(proposedWaypoint, out hit, 1f, NavMesh.AllAreas);
    }

    protected override void OnReset() { }
}
