using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Sprint")]
public class SprintAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        PlayerMovement player = stateMachine.GetComponent<PlayerMovement>();
        player.setModalSpeed(player.getSprintSpeed());
        player.turnSmoothTime = player.turnSmoothTimeSlow;
        stateMachine.GetComponent<PlayerMovement>().currentSoundRadius = stateMachine.GetComponent<PlayerMovement>().sprintSoundRadius;
        player.gameObject.layer = 9;
        //player.GetComponent<Player>().indicator.text = "";
        //Change player animation
        //Change player footstep sounds
    }
}
