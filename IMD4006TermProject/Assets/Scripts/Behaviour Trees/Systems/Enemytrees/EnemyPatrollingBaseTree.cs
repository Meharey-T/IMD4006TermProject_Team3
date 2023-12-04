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
        var StareAtPlayer = new TaskStopAndStare(player, enemy);

        List<BTNode> CheckAreaList = new List<BTNode>();
        List<BTNode> CheckIfVisibleList = new List<BTNode>();
        List<BTNode> ChillOutList = new List<BTNode>();
        List<BTNode> StareAtPlayerList = new List<BTNode>();
        CheckAreaList.Add(CheckArea);
        CheckIfVisibleList.Add(CheckIfVisible);
        ChillOutList.Add(ChillOut);
        StareAtPlayerList.Add(StareAtPlayer);

        //Start the behaviour tree
        BTNode root = new Selector(new List<BTNode>
        {
            //Options that are to be picked first should come first
            //Start player chase sequence
            new Sequence(new List<BTNode>
            {
                //Start by checking if player is in range to be chased
                new CheckIfVisible(enemyMeshAgent, player),
                //If we can see the player, different behaviours based on enemy irritation
                //If they're indifferent
                new Sequence(new List<BTNode>
                {
                    //Check how angry the enemy is
                    new CheckIfIndifferent(enemy),
                    //Stare at the player for 3 full seconds before acting if indifferent
                    new Timer(3f, StareAtPlayerList),
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
                //If they're irritated
                new Sequence(new List<BTNode>
                {
                    //Check how angry the enemy is
                    new CheckIfIrritated(enemy),
                    //Stare at the player for 3 full seconds before acting if indifferent
                    new Timer(1.5f, StareAtPlayerList),
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
                //By process of elimination, any more angry behaviours
                new Sequence(new List<BTNode>
                {
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
                })

            }),
            //If they don't see the player right now, have they seen them? Check out last place seen
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
            //If they don't see and haven't seen, can they or have they heard them? Check out last place heard
            new Sequence(new List<BTNode>
            {
                //Start by seeing if the enemy can currently hear the player
                new CheckIfPlayerAudible(enemy),
                //Then have them go to the location they last heard the player
                new TaskCheckOutSound(enemy, enemyMeshAgent),
                //Spend a few seconds looking around at the last place they were heard
                new Timer(1.5f, CheckAreaList),
                new Timer(1.5f, CheckAreaList),
                new Timer(1.5f, CheckAreaList),
                new TaskClearDetection(enemy, enemyMeshAgent)
                //new TaskCheckArea(transform)
            }),
            //If no player detection conditions are true, by default go back to patrolling
            new TaskFollowPatrol(transform, waypoints, enemyMeshAgent),
            new Timer(3f, ChillOutList)
        });
        return root;
    }
}
