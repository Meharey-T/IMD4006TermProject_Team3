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
        //If we have heard or seen something, we want to stop and look at it for the duration of the timer
        //if (thisActor.seesPlayer || thisActor.hearsPlayer)
        //{
            Vector3 direction = (player.transform.position - thisActor.transform.position).normalized;
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            thisActor.transform.rotation = Quaternion.Slerp(thisActor.transform.rotation, lookRotation, Time.deltaTime * 5f);
            thisActor.hasStopped = true;
            state = NodeState.RUNNING;
        //}
        //If they stop hearing or seeing the player, they may follow up on it immediately instead of waiting
        //else if(thisActor.sawPlayer || thisActor.heardPlayer)
        //{
            //state = NodeState.SUCCESS;
        //}
        return state;
    }

    protected override void OnReset() { }
}
