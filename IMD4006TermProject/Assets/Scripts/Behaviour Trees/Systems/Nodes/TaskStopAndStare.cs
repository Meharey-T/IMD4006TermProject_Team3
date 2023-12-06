using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskStopAndStare : BTNode
{
    Player player;
    Quaternion lookRotation;
    Enemy thisActor;

    public TaskStopAndStare(Player player, Enemy enemy)
    {
        this.player = player;
        thisActor = enemy;
    }

    protected override NodeState OnRun()
    {
        Debug.Log("Running TaskStopAndStare");

        Vector3 direction = (player.transform.position - thisActor.transform.position).normalized;
        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        thisActor.transform.rotation = Quaternion.Slerp(thisActor.transform.rotation, lookRotation, Time.deltaTime * 5f);
        state = NodeState.RUNNING;
        return state;
    }

    protected override void OnReset() { }
}
