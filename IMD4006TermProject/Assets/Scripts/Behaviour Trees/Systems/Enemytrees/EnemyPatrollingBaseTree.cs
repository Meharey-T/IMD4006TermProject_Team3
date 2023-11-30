using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class EnemyPatrollingBaseTree : BTree
{
    private List<GameObject> waypoints;
    private Player player;
    private Enemy enemy;
    private NavMeshAgent enemyMeshAgent;

    protected override BTNode SetupTree()
    {
        enemyMeshAgent = transform.GetComponent<NavMeshAgent>();
        player = transform.GetComponent<Enemy>().playerObj.GetComponent<Player>();
        enemy = transform.GetComponent<Enemy>();
        waypoints = enemy.Waypoints;

        var CheckArea = new TaskCheckArea(transform);
        var CheckIfVisible = new CheckIfVisible(enemyMeshAgent, player);
        var ChillOut = new TaskChillASecond(enemyMeshAgent);

        List<BTNode> CheckAreaList = new List<BTNode>();
        List<BTNode> CheckIfVisibleList = new List<BTNode>();
        List<BTNode> ChillOutList = new List<BTNode>();
        CheckAreaList.Add(CheckArea);
        CheckIfVisibleList.Add(CheckIfVisible);
        ChillOutList.Add(ChillOut);

        //Start the behaviour tree
        BTNode root = new Selector(new List<BTNode>
        {
            //Options that are to be picked first should come first
            //Start player chase sequence
            new Sequence(new List<BTNode>
            {
                //Start by checking if player is in range to be chased
                new CheckIfVisible(enemyMeshAgent, player),
                //Set a waypoint to pursue the player
                new TaskChasePlayer(transform, enemyMeshAgent, player),
                //Set this so that it looks around if chasing the player fails
                new Inverter(CheckIfVisibleList),
                new CheckIfPlayerSeen(enemyMeshAgent, player),
                //path to the last place they saw them first
                new TaskCheckLastPlaceSeen(enemy, enemyMeshAgent),
                //Then stop and look around
                new Timer(1.5f, CheckAreaList),
                new Timer(1.5f, CheckAreaList),
                new Timer(1.5f, CheckAreaList),
                new TaskClearDetection(enemy, enemyMeshAgent)
            }),
            new Sequence(new List<BTNode>
            {
                new CheckIfPlayerSeen(enemyMeshAgent, player),
                //path to the last place they saw them first
                new TaskCheckLastPlaceSeen(enemy, enemyMeshAgent),
                //Then stop and look around
                new Timer(1.5f, CheckAreaList),
                new Timer(1.5f, CheckAreaList),
                new Timer(1.5f, CheckAreaList),
                new TaskClearDetection(enemy, enemyMeshAgent)
            }),
            //Check out sounds they've heard
            new Sequence(new List<BTNode>
            {
                //Start by seeing if the enemy can currently hear the player
                new CheckIfPlayerAudible(enemy),
                //Then have them go to the location they last heard the player
                new TaskCheckOutSound(enemy, enemyMeshAgent),
                //We might add a short task for them to spend a certain amount of time looking around
                new Timer(1.5f, CheckAreaList),
                new Timer(1.5f, CheckAreaList),
                new Timer(1.5f, CheckAreaList),
                new TaskClearDetection(enemy, enemyMeshAgent)
                //new TaskCheckArea(transform)
            }),
            //Start idle patrol sequence
            new TaskFollowPatrol(transform, waypoints, enemyMeshAgent),
            new Timer(3f, ChillOutList)
        });
        return root;
    }
}
