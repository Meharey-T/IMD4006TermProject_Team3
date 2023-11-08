using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Hide")]
public class HideAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        PlayerMovement player = stateMachine.GetComponent<PlayerMovement>();
        player.setModalSpeed(0);
        SphereCollider soundRadius = stateMachine.GetComponent<SphereCollider>();
        soundRadius.radius = 0;
        player.gameObject.layer = 8;
        player.transform.position = player.GetComponent<Player>().nearestHideable;
        player.GetComponent<Player>().indicator.text = "Hiding";
        //Change player animation
    }
}