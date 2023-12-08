using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Sprint")]
public class SprintAction : FSMAction
{
    BoxCollider[] colliders;
    Vector3 colliderSize = new Vector3(0, 0.68f, 0);
    Vector3 colliderOffset = new Vector3(0.5f, 1.35f, 0.5f);

    public override void Execute(BaseStateMachine stateMachine)
    {
        PlayerMovement player = stateMachine.GetComponent<PlayerMovement>();
        TerrainState terraine = stateMachine.GetComponent<TerrainState>();
        
        stateMachine.GetComponent<TerrainState>().currSpeed = stateMachine.GetComponent<TerrainState >().GetRun();
        player.setModalSpeed(player.getSprintSpeed());
        player.turnSmoothTime = player.turnSmoothTimeSlow;
        stateMachine.GetComponent<PlayerMovement>().currentSoundRadius = stateMachine.GetComponent<PlayerMovement>().sprintSoundRadius;
        player.gameObject.layer = 9;
        player.consumingStamina = true;
        colliders = player.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider c in colliders){
            c.center = colliderSize;
            c.size = colliderOffset;
        }

        if (player.groundedPlayer && !player.isJumping)
        {
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfSprintingHash, true);
            //Set all the other ones to false
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfSneakingHash, false);
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfHidingHash, false);
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfWalkingHash, false);
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfJumpingHash, false);
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfRollingHash, false);
        }

        //Jump if we're running
        if (Input.GetButtonDown("Jump") && player.groundedPlayer && player.currentStamina >= 50)
        {
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfSprintingHash, false);
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfJumpingHash, true);
            player.rb.AddForce(Vector3.up * player.jumpAmount, ForceMode.Impulse);
            player.isJumping = true;
            player.currentStamina -= 30;
            player.StartCoroutine(player.ResetJump());
        }

        
        //player.currentAnimation = player.animationLibrary.animationList[1];
        //Change player footstep sounds
        //terraine.currSpeed = terraine.GetRun();
        //; = stateMachine.GetComponent<TerrainState>().GetRun() ;
    }
}
