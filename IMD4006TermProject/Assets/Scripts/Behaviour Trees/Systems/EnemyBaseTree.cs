using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class EnemyBaseTree : Tree
{
    private Transform[] Waypoints;
    private float waypointRadius = 5;
    //private Vector3 nextWaypointPos;
    //private Vector3 position;
    private Player player;
    private Enemy enemy;
    private NavMeshAgent enemyMeshAgent;

    protected override BTNode SetupTree()
    {
        enemyMeshAgent = transform.GetComponent<NavMeshAgent>();
        player = transform.GetComponent<Enemy>().playerObj.GetComponent<Player>();
        BTNode root = new Selector(new List<BTNode>
        {
            //Options that are to be picked first should come first
            //Start player chase sequence
            new Sequence(new List<BTNode>
            {
                //Start by checking if player is in range to be chased
                new CheckIfInRange(enemyMeshAgent, player),
                //Set a waypoint to pursue the player
                new GoToWaypoint(transform, enemyMeshAgent, player.transform.position)
            }),
            //Start idle patrol sequence
            new GoToWaypoint(transform, waypointRadius, enemyMeshAgent),
        }) ;
        return root;
    }
}
