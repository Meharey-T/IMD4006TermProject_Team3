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
        SphereCollider soundRadius = stateMachine.GetComponentInChildren<SphereCollider>();
        soundRadius.radius = 2f;
        player.gameObject.layer = 9;
        //player.GetComponent<Player>().indicator.text = "";
        //Change player animation
        //Change player footstep sounds
    }
}
