using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskCheckArea : BTNode
{
    Transform BTTransform;
    int waypointRadius = 1;

    public TaskCheckArea(Transform transform)
    {
        BTTransform = transform;
    }

    protected override NodeState OnRun()
    {
        //If we see or hear the player, we stop doing this right away
        if (BTTransform.GetComponent<Enemy>().seesPlayer || BTTransform.GetComponent<Enemy>().hearsPlayer)
        {
            state = NodeState.FAILURE;
        }

        //Make a random point to turn towards
        Vector3 turnToPoint = CreateTurnToPoint();
        /*
        Vector3 direction = (turnToPoint - BTTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        BTTransform.rotation = Quaternion.Slerp(BTTransform.rotation, lookRotation, Time.deltaTime * 5f);
        //turnCount++;
        Debug.Log(turnCount);
        */

        //Turn towards it
        BTTransform.LookAt(turnToPoint, Vector3.up);
        state = NodeState.SUCCESS;
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
