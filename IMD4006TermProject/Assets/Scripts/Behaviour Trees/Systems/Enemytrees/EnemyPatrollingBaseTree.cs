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

        //Start the behaviour tree
        BTNode root = new Selector(new List<BTNode>
        {
            //Options that are to be picked first should come first

            ////CHASE PLAYER IF SPOTTED SEQUENCE
            new Sequence(new List<BTNode>
            {
                //Start by checking if player is in range to be chased
                new CheckIfVisible(enemyMeshAgent),
                //If we can see the player, different behaviours based on enemy irritation
                //If they're indifferent
                new Sequence(new List<BTNode>
                {
                    //Check how angry the enemy is
                    new CheckIfIndifferent(enemy),
                    //Check if they've already stopped before
                    new Inverter(new CheckIfStopped(enemy)),
                    //Stare at the player for 3 full seconds before acting if indifferent
                    new Timer(2f, new TaskStopAndStare(player, enemy)),
                    //Set a waypoint to pursue the player
                    new TaskChasePlayer(transform, enemyMeshAgent, player),
                    //Set this so that it looks around if chasing the player fails
                    new Inverter(new CheckIfVisible(enemyMeshAgent)),
                    new CheckIfPlayerSeen(enemyMeshAgent),
                    //path to the last place they saw them first
                    new TaskCheckLastPlaceSeen(enemy, enemyMeshAgent),
                    //Then stop and look around
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new TaskClearDetection(enemy, enemyMeshAgent)
                }),
                //If they're irritated
                new Sequence(new List<BTNode>
                {
                    //Check how angry the enemy is
                    new CheckIfIrritated(enemy),
                    //Check if they've already stopped before
                    new Inverter(new CheckIfStopped(enemy)),
                    //Stare at the player for 3 full seconds before acting if indifferent
                    new Timer(1f, new TaskStopAndStare(player, enemy)),
                    //Set a waypoint to pursue the player
                    new TaskChasePlayer(transform, enemyMeshAgent, player),
                    //Set this so that it looks around if chasing the player fails
                    new Inverter(new CheckIfVisible(enemyMeshAgent)),
                    new CheckIfPlayerSeen(enemyMeshAgent),
                    //path to the last place they saw them first
                    new TaskCheckLastPlaceSeen(enemy, enemyMeshAgent),
                    //Then stop and look around
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new TaskClearDetection(enemy, enemyMeshAgent)
                }),
                //By process of elimination, any more angry behaviours
                new Sequence(new List<BTNode>
                {
                    //Set a waypoint to pursue the player
                    new TaskChasePlayer(transform, enemyMeshAgent, player),
                    //Set this so that it looks around if chasing the player fails
                    new Inverter(new CheckIfVisible(enemyMeshAgent)),
                    new CheckIfPlayerSeen(enemyMeshAgent),
                    //path to the last place they saw them first
                    new TaskCheckLastPlaceSeen(enemy, enemyMeshAgent),
                    //Then stop and look around
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new TaskClearDetection(enemy, enemyMeshAgent)
                })

            }),

            ////INVESTIGATE SOUNDS SEQUENCE
            new Sequence(new List<BTNode>
            {
                //Start by seeing if the enemy can currently hear the player
                new CheckIfPlayerAudible(enemy),
                //If they can, how angry are they?
                new Sequence(new List<BTNode>
                {
                    new CheckIfIndifferent(enemy),
                    //Check if they've already stopped before
                    new Inverter(new CheckIfStopped(enemy)),
                    //Stop and stare for a moment before chasing
                    new Timer(2f, new TaskStopAndStare(player, enemy)),
                    //Then have them go to the location they last heard the player
                    new TaskCheckOutSound(enemy, enemyMeshAgent),
                    //Spend a few seconds looking around at the last place they were heard
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new TaskClearDetection(enemy, enemyMeshAgent)
                }),
                new Sequence(new List<BTNode>
                {
                    new CheckIfIrritated(enemy),
                    //Check if they've already stopped before
                    new Inverter(new CheckIfStopped(enemy)),
                    //Stop and stare for a moment before chasing
                    new Timer(1f, new TaskStopAndStare(player, enemy)),
                    //Then have them go to the location they last heard the player
                    new TaskCheckOutSound(enemy, enemyMeshAgent),
                    //Spend a few seconds looking around at the last place they were heard
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new TaskClearDetection(enemy, enemyMeshAgent)
                }),
                new Sequence(new List<BTNode>
                {
                    //Then have them go to the location they last heard the player
                    new TaskCheckOutSound(enemy, enemyMeshAgent),
                    //Spend a few seconds looking around at the last place they were heard
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new Timer(1.5f, new TaskCheckArea(transform)),
                    new TaskClearDetection(enemy, enemyMeshAgent)
                })

            }),

            ////INVESTIGATE LAST PLACE SEEN SEQUENCE
            new Sequence(new List<BTNode>
            {
                new CheckIfPlayerSeen(enemyMeshAgent),
                //path to the last place they saw them first
                new TaskCheckLastPlaceSeen(enemy, enemyMeshAgent),
                //Then stop and look around
                new Timer(1.5f, new TaskCheckArea(transform)),
                new Timer(1.5f, new TaskCheckArea(transform)),
                new Timer(1.5f, new TaskCheckArea(transform)),
                new TaskClearDetection(enemy, enemyMeshAgent)
            }),

            ////INVESTIGATE LAST PLACE HEARD SEQUENCE
            new Sequence(new List<BTNode>
            {
                new CheckIfPlayerHeard(enemy),
                //path to the last place they saw them first
                new TaskCheckOutSound(enemy, enemyMeshAgent),
                //Then stop and look around
                new Timer(1f, new TaskCheckArea(transform)),
                new Timer(1f, new TaskCheckArea(transform)),
                new Timer(1f, new TaskCheckArea(transform)),
                new TaskClearDetection(enemy, enemyMeshAgent)
            }),
            
            ////PATROL SEQUENCE
            new TaskFollowPatrol(transform, waypoints, enemyMeshAgent),
            new Timer(3f, new TaskChillASecond(enemyMeshAgent))
        });
        return root;
    }
}
