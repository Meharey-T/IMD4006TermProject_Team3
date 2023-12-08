using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;

public class TaskClearDetection : BTNode
{
    NavMeshAgent agent;
    Enemy thisActor;

    public TaskClearDetection(Enemy enemy, NavMeshAgent enemyMeshAgent)
    {
        thisActor = enemy;
        agent = enemyMeshAgent;
    }

    protected override NodeState OnRun()
    {

        //HEARD PLAYER
        //If this enemy hear the player, set their states and play their soundFX
        if (thisActor.heardPlayer)
        {
            if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetIndifference();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetHeardState();
                agent.GetComponent<EnemySoundFX>().PlayCheckOutSoundSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetIrritated();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetHeardState();
                agent.GetComponent<EnemySoundFX>().PlayCheckOutSoundSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.ANGRY)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetAngry();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetHeardState();
                agent.GetComponent<EnemySoundFX>().PlayCheckOutSoundSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.FURIOUS)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetFurious();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetHeardState();
                agent.GetComponent<EnemySoundFX>().PlayCheckOutSoundSound();
            }
        }


        //SEEN PLAYER
        //If this enemy hear the player, set their states and play their soundFX
        if (thisActor.sawPlayer)
        {
            if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetIndifference();
                //agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetSawState();
                agent.GetComponent<EnemySoundFX>().PlayCheckOutSawSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetIrritated();
                //agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetSawState();
                agent.GetComponent<EnemySoundFX>().PlayCheckOutSawSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.ANGRY)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetAngry();
                //agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetSawState();
                agent.GetComponent<EnemySoundFX>().PlayCheckOutSawSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.FURIOUS)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetFurious();
                //agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetSawState();
                agent.GetComponent<EnemySoundFX>().PlayCheckOutSawSound();
            }
        }


        //SEE PLAYER
        //If this enemy hear the player, set their states and play their soundFX
        if (thisActor.seesPlayer)
        {
            if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetIndifference();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetSawState();

                agent.GetComponent<EnemySoundFX>().PlaySeeSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetIrritated();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetSawState();

                agent.GetComponent<EnemySoundFX>().PlaySeeSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.ANGRY)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetAngry();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetSawState();
                agent.GetComponent<EnemySoundFX>().PlaySeeSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.FURIOUS)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetFurious();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetSawState();
                agent.GetComponent<EnemySoundFX>().PlaySeeSound();
            }
        }

        //HEAR PLAYER
        //If this enemy hear the player, set their states and play their soundFX
        if (thisActor.hearsPlayer)
        {
            if (thisActor.angerLevel == Enemy.AngerLevel.INDIFFERENT)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetIndifference();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetHearState();
                agent.GetComponent<EnemySoundFX>().PlayHearSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.IRRITATED)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetIrritated();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetHearState();
                agent.GetComponent<EnemySoundFX>().PlayHearSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.ANGRY)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetAngry();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetHearState();
                agent.GetComponent<EnemySoundFX>().PlayHearSound();
            }
            if (thisActor.angerLevel == Enemy.AngerLevel.FURIOUS)
            {
                //check and set enemies emotion, set their state, play their sound
                agent.GetComponent<EnemySoundFX>().currEmotion = agent.GetComponent<EnemySoundFX>().GetFurious();
                agent.GetComponent<EnemySoundFX>().currState = agent.GetComponent<EnemySoundFX>().GetHearState();
                agent.GetComponent<EnemySoundFX>().PlayHearSound();
            }
        
        }

        agent.ResetPath();
        agent.GetComponent<Enemy>().enemyAnimator.animator.SetBool(agent.GetComponent<Enemy>().enemyAnimator.IfWalkingHash, false);
        agent.GetComponent<Enemy>().enemyAnimator.animator.SetBool(agent.GetComponent<Enemy>().enemyAnimator.IfSprintingHash, false);
        agent.GetComponent<Enemy>().enemyAnimator.animator.SetBool(agent.GetComponent<Enemy>().enemyAnimator.IfTurningLeftHash, false);
        agent.GetComponent<Enemy>().enemyAnimator.animator.SetBool(agent.GetComponent<Enemy>().enemyAnimator.IfTurningRightHash, false);
        agent.GetComponent<Enemy>().enemyAnimator.animator.SetBool(agent.GetComponent<Enemy>().enemyAnimator.IfGrabbingHash, false);

        thisActor.hearsPlayer = false;
        thisActor.seesPlayer = false;
        thisActor.sawPlayer = false;
        thisActor.heardPlayer = false;
        thisActor.hasStopped = false;
        thisActor.caughtPlayer = false;
        state = NodeState.SUCCESS;

        return state;
    }

    protected override void OnReset() {

    }
}
