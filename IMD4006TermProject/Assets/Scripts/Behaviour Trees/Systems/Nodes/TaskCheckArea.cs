using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskCheckArea : BTNode
{
    Transform BTTransform;
    int waypointRadius = 1;
    bool setWayPoint = false;
    Quaternion lookRotation;

    public TaskCheckArea(Transform transform)
    {
        BTTransform = transform;
    }

    protected override NodeState OnRun()
    {
        //Debug.Log("Running TaskCheckArea");
        //If we see the player, we stop doing this right away
        if (BTTransform.GetComponent<Enemy>().seesPlayer)
        {
            state = NodeState.FAILURE;
        }
        //Otherwise we need to set up an initial turn direction
        else if (!setWayPoint)
        {
            Vector3 turnToPoint = CreateTurnToPoint();
            Vector3 direction = turnToPoint.normalized;
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            state = NodeState.RUNNING;
            setWayPoint = true;
        }
        //If we've already set that, then we turn to it
        else if (setWayPoint){
            BTTransform.rotation = Quaternion.Slerp(BTTransform.rotation, lookRotation, Time.deltaTime * 5f);
            state = NodeState.RUNNING;
        }
        return state;
    }

    private Vector3 CreateTurnToPoint()
    {
        float waypointZ = Random.Range(-waypointRadius, waypointRadius);
        float waypointX = Random.Range(-waypointRadius, waypointRadius);
        Vector3 proposedWaypoint = new Vector3(waypointX + BTTransform.position.x, this.BTTransform.position.y, waypointZ + BTTransform.position.z);
        return proposedWaypoint;
    }

    protected override void OnReset() { }
}
