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
        SphereCollider soundRadius = stateMachine.GetComponentInChildren<SphereCollider>();
        if(stateMachine.GetComponent<Player>().previousPosition != new Vector3(0, 0, 0))
        {
            stateMachine.transform.position = stateMachine.GetComponent<Player>().previousPosition;
            stateMachine.GetComponent<Player>().previousPosition = new Vector3(0, 0, 0);
            stateMachine.GetComponent<Player>().hiding = false;
        }
        soundRadius.radius = 5;
        player.gameObject.layer = 9;
        //player.GetComponent<Player>().indicator.text = "";
        //Change player animation
        //Change player footstep sounds
    }
}

