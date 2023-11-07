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
        enemy = transform.GetComponent<Enemy>();

        //Start the behaviour tree
        BTNode root = new Selector(new List<BTNode>
        {
            //Options that are to be picked first should come first
            //Start player chase sequence
            new Sequence(new List<BTNode>
            {
                //Start by checking if player is in range to be chased
                new CheckIfInRange(enemyMeshAgent, player),
                //Set a waypoint to pursue the player
                new TaskChasePlayer(transform, enemyMeshAgent, player)
            }),
            //Check out sounds they've heard
            new Sequence(new List<BTNode>
            {
                //Start by seeing if the enemy can currently hear the player
                new CheckIfPlayerAudible(enemy),
                //Then have them go to the location they last heard the player
                new TaskCheckOutSound(enemy, enemyMeshAgent)
                //We might add a short task for them to spend a certain amount of time looking around
            }),
            //Start idle patrol sequence
            new TaskRandomWander(transform, waypointRadius, enemyMeshAgent),
        }) ;
        return root;
    }
}
