using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Walk")]
public class WalkAction : FSMAction
{
    BoxCollider[] colliders;
    Vector3 colliderSize = new Vector3(0, 0.68f, 0);
    Vector3 colliderOffset = new Vector3(0.5f, 1.35f, 0.5f);

    public override void Execute(BaseStateMachine stateMachine)
    {
        PlayerMovement player = stateMachine.GetComponent<PlayerMovement>();
        player.setModalSpeed(player.getBaseSpeed());
        player.turnSmoothTime = player.turnSmoothTimeSnappy;
        if(stateMachine.GetComponent<Player>().previousPosition != new Vector3(0, 0, 0))
        {
            stateMachine.transform.position = stateMachine.GetComponent<Player>().previousPosition;
            stateMachine.GetComponent<Player>().previousPosition = new Vector3(0, 0, 0);
            stateMachine.GetComponent<Player>().hiding = false;
        }
        player.currentSoundRadius = player.walkSoundRadius;
        player.gameObject.layer = 9;
        player.soundRadius.gameObject.layer = 9;
        TerrainState terraine = stateMachine.GetComponent<TerrainState>();

        stateMachine.GetComponent<TerrainState>().currSpeed = stateMachine.GetComponent<TerrainState>().GetWalk();
        player.consumingStamina = false;

        //play sneak sfx
        terraine.PlayWalkingSound();

        if (player.groundedPlayer && !player.isJumping)
        {
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfWalkingHash, true);
            //Set all the other ones to false
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfSprintingHash, false);
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfSneakingHash, false);
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfHidingHash, false);
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfRollingHash, false);
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfJumpingHash, false);
        }

        //jump if we're walking
        if (Input.GetButtonDown("Jump") && player.groundedPlayer && player.currentStamina >= 50)
        {
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfJumpingHash, true);
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfWalkingHash, false);
            player.rb.AddForce(Vector3.up * player.jumpAmount, ForceMode.Impulse);
            player.isJumping = true;
            player.currentStamina -= 30;
            player.StartCoroutine(player.ResetJump());
        }
        
        colliders = player.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider c in colliders)
        {
            c.center = colliderSize;
            c.size = colliderOffset;
        }
        //player.currentAnimation = player.animationLibrary.animationList[0];
        //Change player animation
        //Change player footstep sounds
    }
}

