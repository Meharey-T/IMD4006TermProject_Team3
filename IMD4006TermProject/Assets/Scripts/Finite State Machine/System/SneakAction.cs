using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Sneak")]
public class SneakAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        PlayerMovement player = stateMachine.GetComponent<PlayerMovement>();
        player.setModalSpeed(player.getSneakSpeed());
        SphereCollider soundRadius = stateMachine.GetComponent<SphereCollider>();
        soundRadius.radius = 2f;
        //Change player animation
        //Change player footstep sounds
    }
}
