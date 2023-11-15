using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskCheckArea : BTNode
{
    Transform BTTransform;
    int waypointRadius = 1;
    int turnCount = 0;

    public TaskCheckArea(Transform transform)
    {
        BTTransform = transform;
    }

    protected override NodeState OnRun()
    {
        /*
        if (turnCount == 3)
        {
            Debug.Log("Looked around 3 times");
            state = NodeState.SUCCESS;
            turnCount = 0;
        }
        else if (turnCount < 3)
        {
            Vector3 turnToPoint = CreateTurnToPoint();
            Vector3 direction = (turnToPoint - BTTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            BTTransform.rotation = Quaternion.Slerp(BTTransform.rotation, lookRotation, Time.deltaTime * 5f);
            turnCount++;
            Debug.Log(turnCount);
            state = NodeState.RUNNING;
        }
        */
        if (BTTransform.GetComponent<Enemy>().seesPlayer || BTTransform.GetComponent<Enemy>().hearsPlayer)
        {
            state = NodeState.FAILURE;
        }
        Vector3 turnToPoint = CreateTurnToPoint();
        /*
        Vector3 direction = (turnToPoint - BTTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        BTTransform.rotation = Quaternion.Slerp(BTTransform.rotation, lookRotation, Time.deltaTime * 5f);
        //turnCount++;
        Debug.Log(turnCount);
        */
        BTTransform.LookAt(turnToPoint);
        state = NodeState.SUCCESS;
        return state;
    }

    private Vector3 CreateTurnToPoint()
    {
        float waypointZ = Random.Range(0, waypointRadius);
        float waypointX = Random.Range(0, waypointRadius);
        Vector3 proposedWaypoint = new Vector3(waypointX, 0, waypointZ);
        return proposedWaypoint;
    }

    protected override void OnReset() { }
}
