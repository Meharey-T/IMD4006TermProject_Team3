using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

//Give the AI a set of predetermined waypoints to follow on a set order
public class TaskFollowPatrol : BTNode
{

    Transform BTTransform;
    //Vector3 nextWaypointPos;
    NavMeshAgent agent;
    List<GameObject> waypointList;
    int waypointIndex;

    public TaskFollowPatrol(Transform transform, List<GameObject> waypointList, NavMeshAgent enemyAgent)
    {
        this.waypointList = waypointList;
        BTTransform = transform;
        agent = enemyAgent;
        waypointIndex = 0;
    }

    protected override NodeState OnRun()
    {
        //If at some point we can see or hear the player, stop what we're doing and switch to that instead
        if (agent.GetComponent<Enemy>().seesPlayer || agent.GetComponent<Enemy>().hearsPlayer
            || agent.GetComponent<Enemy>().sawPlayer || agent.GetComponent<Enemy>().heardPlayer)
        {
            state = NodeState.FAILURE;
        }
        else if (waypointIndex == waypointList.Count || !waypointList[waypointIndex])
        {
            waypointIndex = 0;
            state = NodeState.SUCCESS;
        }
        else if (waypointList[waypointIndex]){
            float waypointDistance = Vector3.Distance(BTTransform.position, waypointList[waypointIndex].transform.position);
            if (waypointDistance < 1) {
                agent.SetDestination(waypointList[waypointIndex].transform.position);
                //Debug.Log("Reached waypoint");
                waypointIndex++;
                state = NodeState.RUNNING;
            }
            //If we're not there yet, keep going
            else if (waypointDistance >= 1)
            {
                agent.SetDestination(waypointList[waypointIndex].transform.position);
                state = NodeState.RUNNING;
            }
        }
        //If we've reached the current waypoint, succeed and move to the next one
        
        return state;
    }

    protected override void OnReset() { }
}
