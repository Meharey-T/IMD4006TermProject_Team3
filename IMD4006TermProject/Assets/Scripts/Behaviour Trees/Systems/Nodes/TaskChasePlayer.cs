using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

//This behaviour node has the character actually chase down the player
public class TaskChasePlayer : BTNode
{
    Transform transform;
    NavMeshAgent agent;
    Player player;
    Enemy thisActor;
    Quaternion lookRotation;

    public TaskChasePlayer(Transform transform, NavMeshAgent enemyMeshAgent, Player player)
    {
        this.transform = transform;
        agent = enemyMeshAgent;
        this.player = player;
        thisActor = agent.GetComponent<Enemy>();
    }

    protected override NodeState OnRun()
    {
        //Debug.Log("Running TaskChasePlayer");
        float waypointDistance = Vector3.Distance(transform.position, player.transform.position);
        if (agent.speed == thisActor.defaultSpeed)
        {
            if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
            {
                agent.speed = 3.5f;
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, true);
            }
            else if (thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
            {
                agent.speed = 4f;
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfWalkingHash, true);
            }
            else if (thisActor.angerLevel == Enemy.AngerLevel.ANGRY)
            {
                agent.speed = 5f;
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, true);
            }
            else if (thisActor.angerLevel == Enemy.AngerLevel.FURIOUS)
            {
                agent.speed = 6f;
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfSprintingHash, true);
            }
        }

        //Debug.Log("This character sees player: " + thisActor.seesPlayer);

        if (waypointDistance >= 1 && thisActor.seesPlayer)
        {
            Vector3 direction = (player.transform.position - thisActor.transform.position).normalized;
            if(direction.z < 0)
            {
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningLeftHash, true);
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningRightHash, false);
            }
            else if(direction.z > 0)
            {
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningRightHash, true);
                thisActor.enemyAnimator.animator.SetBool(thisActor.enemyAnimator.IfTurningLeftHash, false);
            }
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            thisActor.transform.rotation = Quaternion.Slerp(thisActor.transform.rotation, lookRotation, Time.deltaTime * 5f);
            //Check if the player is standing on something above the ground
            if (player.transform.position.y > 0.3 && player.GetComponent<PlayerMovement>().groundedPlayer)
            {
                Vector3 closestNavPoint = FindNearestPathable();
                agent.SetDestination(closestNavPoint);
            }
            else
            {
                agent.SetDestination(player.transform.position);
            }

            // Debug.Log("going to new position");
            state = NodeState.RUNNING;
        }
        //If we're basically on the player now
        else if (waypointDistance < 1)
        {
            agent.speed = thisActor.defaultSpeed;
            state = NodeState.SUCCESS;
        }
        //If at any point the enemy can no longer see the player, this function fails and we change behaviour
        else if(!thisActor.seesPlayer)
        {
            thisActor.sawPlayer = true;
            agent.speed = thisActor.defaultSpeed;
            state = NodeState.FAILURE;
        }
        //If we can still see the player but haven't reached them yet
        
        return state;
    }

    private Vector3 FindNearestPathable()
    {
        //Try to find a point near the player on the ground
        
        Vector3 testPoint = new Vector3(player.transform.position.x, 0, player.transform.position.z) + Random.insideUnitSphere * 1.5f;
        while (!TestPoint(testPoint))
        {
            testPoint = new Vector3(player.transform.position.x, 0, player.transform.position.z) + Random.insideUnitSphere * 1.5f;
        }
        return testPoint;
        /*
        NavMeshHit hit;
        if(NavMesh.FindClosestEdge(player.transform.position, out hit, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return hit.position;
        */
    }
    private bool TestPoint(Vector3 proposedWaypoint)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(proposedWaypoint, out hit, 5f, NavMesh.AllAreas);
    }

    protected override void OnReset() { }
}
