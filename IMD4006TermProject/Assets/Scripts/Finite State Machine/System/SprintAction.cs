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
        SphereCollider soundRadius = stateMachine.GetComponentInChildren<SphereCollider>();
        soundRadius.radius = 9;
        player.gameObject.layer = 9;
        //player.GetComponent<Player>().indicator.text = "";
        //Change player animation
        //Change player footstep sounds
    }
}
