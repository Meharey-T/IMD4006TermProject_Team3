using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskRandomWander : BTNode
{
    Transform BTTransform;
    Vector3 nextWaypointPos;
    NavMeshAgent agent;
    float waypointRadius;

    public TaskRandomWander(Transform transform, float waypointRadius, NavMeshAgent enemyAgent)
    {
        this.waypointRadius = waypointRadius;
        BTTransform = transform;
        agent = enemyAgent;
        NewPatrolPoint();
    }

    protected override NodeState OnRun()
    {
        float waypointDistance = Vector3.Distance(BTTransform.position, nextWaypointPos);

        if (agent.GetComponent<Enemy>().seesPlayer || agent.GetComponent<Enemy>().hearsPlayer)
        {
            state = NodeState.FAILURE;
        }

        if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            NewPatrolPoint();
        }

        if (waypointDistance < 1)
        {
            Debug.Log("Reached waypoint");
            state = NodeState.SUCCESS;
            NewPatrolPoint();
        }
        else if (waypointDistance >= 1)
        {
            agent.SetDestination(nextWaypointPos);
            state = NodeState.RUNNING;
        }
        return state;
    }

    private void NewPatrolPoint()
    {
        float waypointZ = Random.Range(-waypointRadius, waypointRadius);
        float waypointX = Random.Range(-waypointRadius, waypointRadius);
        Vector3 proposedWaypoint = new Vector3(waypointX + BTTransform.position.x, 1, waypointZ + BTTransform.position.z);

        while (!TestPoint(proposedWaypoint))
        {
            waypointZ = Random.Range(-waypointRadius, waypointRadius);
            waypointX = Random.Range(-waypointRadius, waypointRadius);
            proposedWaypoint = new Vector3(waypointX + BTTransform.position.x, 1, waypointZ + BTTransform.position.z);
        }
        nextWaypointPos.Set(waypointX + BTTransform.position.x, 1, waypointZ + BTTransform.position.z);
    }

    private bool TestPoint(Vector3 proposedWaypoint)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(proposedWaypoint, out hit, 1f, NavMesh.AllAreas);
    }

    protected override void OnReset() { }

}
