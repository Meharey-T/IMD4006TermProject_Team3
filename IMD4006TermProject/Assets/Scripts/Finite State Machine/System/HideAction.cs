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
        SphereCollider soundRadius = stateMachine.GetComponentInChildren<SphereCollider>();
        soundRadius.radius = 0;
        player.gameObject.layer = 8;
        if(stateMachine.GetComponent<Player>().previousPosition == new Vector3(0, 0, 0))
        {
            stateMachine.GetComponent<Player>().previousPosition = new Vector3(player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z);
        }
        player.transform.position = player.GetComponent<Player>().selectedHideable;
        if (!stateMachine.GetComponent<Player>().hiding)
        {
            stateMachine.GetComponent<Player>().hiding = true;
        }
        //Change player animation
    }
}
