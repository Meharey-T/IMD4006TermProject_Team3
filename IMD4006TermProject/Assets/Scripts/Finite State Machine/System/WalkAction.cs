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
        SphereCollider soundRadius = stateMachine.GetComponent<SphereCollider>();
        soundRadius.radius = 5;
        //Change player animation
        //Change player footstep sounds
    }
}

