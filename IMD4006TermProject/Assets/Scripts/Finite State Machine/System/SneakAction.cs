using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Sneak")]
public class SneakAction : FSMAction
{
    BoxCollider[] colliders;
    Vector3 colliderSize = new Vector3(0, 0.7f, 0);
    Vector3 colliderOffset = new Vector3(0.5f, 1.4f, 0.5f);

    public override void Execute(BaseStateMachine stateMachine)
    {
        stateMachine.GetComponent<TerrainState>().currSpeed = stateMachine.GetComponent<TerrainState>().GetSneak();
        /*
        if (stateMachine.GetComponent<TerrainState>().currSpeed == stateMachine.GetComponent<TerrainState>().GetSneak())
        { Debug.Log("I should be sneaking"); }
        */
        PlayerMovement player = stateMachine.GetComponent<PlayerMovement>();
        player.setModalSpeed(player.getSneakSpeed());
        player.turnSmoothTime = player.turnSmoothTimeSnappy;
        stateMachine.GetComponent<PlayerMovement>().currentSoundRadius = stateMachine.GetComponent<PlayerMovement>().sneakSoundRadius;
        player.gameObject.layer = 9;
        player.consumingStamina = false;

        foreach (BoxCollider c in colliders)
        {
            c.center = colliderSize;
            c.size = colliderOffset;
        }

        //Forward roll if we're sneaking
        if (Input.GetButtonDown("Jump") && player.groundedPlayer && player.currentStamina >= 50)
        {
            player.rb.AddForce(player.transform.forward * player.jumpAmount * 1f, ForceMode.Impulse);
            player.currentStamina -= 30;
            player.playerAnimator.animator.SetBool(player.playerAnimator.IfRollingHash, true);
        }
        //player.GetComponent<Player>().indicator.text = "";
        //The horrible animation block

        player.playerAnimator.animator.SetBool(player.playerAnimator.IfSneakingHash, true);
        //Set all the other ones to false
        player.playerAnimator.animator.SetBool(player.playerAnimator.IfHidingHash, false);
        player.playerAnimator.animator.SetBool(player.playerAnimator.IfWalkingHash, false);
        player.playerAnimator.animator.SetBool(player.playerAnimator.IfSprintingHash, false);
        player.playerAnimator.animator.SetBool(player.playerAnimator.IfJumpingHash, false);
        //Change player footstep sounds
    }
}
