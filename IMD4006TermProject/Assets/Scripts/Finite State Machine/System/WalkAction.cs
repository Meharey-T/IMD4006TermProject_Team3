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
        //Change player animation
        //Change player footstep sounds
    }
}

