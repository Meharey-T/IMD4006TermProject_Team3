using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

//Give the AI a set of predetermined waypoints to follow on a set order
public class TaskFollowPatrol : BTNode
{

    Transform BTTransform;
    NavMeshAgent agent;
    List<GameObject> waypointList;
    int waypointIndex;
    Enemy thisActor;

    public TaskFollowPatrol(Transform transform, List<GameObject> waypointList, NavMeshAgent enemyAgent)
    {
        this.waypointList = waypointList;
        BTTransform = transform;
        agent = enemyAgent;
        waypointIndex = 0;
        thisActor = agent.GetComponent<Enemy>();
    }

    protected override NodeState OnRun()
    {
        thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningLeftHash, false);
        thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningRightHash, false);
        thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, false);
        //agent.GetComponent<Enemy>().enemyAnimator.animator.SetBool(agent.GetComponent<Enemy>().enemyAnimator.IfGrabbingHash, false);
        //If at some point we can see or hear the player, stop what we're doing and switch to that instead
        if (agent.GetComponent<Enemy>().seesPlayer || agent.GetComponent<Enemy>().hearsPlayer
            || agent.GetComponent<Enemy>().sawPlayer || agent.GetComponent<Enemy>().heardPlayer)
        {
            agent.ResetPath();
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, false);
            state = NodeState.FAILURE;
        }
        else if (thisActor.caughtPlayer)
        {
            agent.ResetPath();
            state = NodeState.FAILURE;
        }
        else if (waypointIndex == waypointList.Count || !waypointList[waypointIndex])
        {
            waypointIndex = 0;
            state = NodeState.SUCCESS;
        }
        else if (waypointList[waypointIndex]){
            thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, true);
            float waypointDistance = Vector3.Distance(BTTransform.position, waypointList[waypointIndex].transform.position);
            //If we've reached the current waypoint, succeed and move to the next one
            if (waypointDistance < 1) {
                agent.SetDestination(waypointList[waypointIndex].transform.position);
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
        return state;
    }

    protected override void OnReset() { }
}
