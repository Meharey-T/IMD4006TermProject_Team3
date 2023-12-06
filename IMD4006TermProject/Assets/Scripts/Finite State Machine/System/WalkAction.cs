using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Walk")]
public class WalkAction : FSMAction
{
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

        //jump if we're walking
        if (Input.GetButtonDown("Jump") && player.groundedPlayer && player.currentStamina >= 30)
        {
            player.rb.AddForce(Vector3.up * player.jumpAmount, ForceMode.Impulse);
            player.currentStamina -= 30;
        }

        //Change player animation
        //Change player footstep sounds
    }
}

