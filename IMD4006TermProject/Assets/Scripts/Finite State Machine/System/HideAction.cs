using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Hide")]
public class HideAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        PlayerMovement player = stateMachine.GetComponent<PlayerMovement>();
        //Player has no move speed
        player.setModalSpeed(0);
        //Player makes no sound
        player.currentSoundRadius = 0;
        //Player can't be collided with as normal
        player.gameObject.layer = 8;
        player.soundRadius.gameObject.layer = 8;
        //Set their previous position, so they return to it more ceremoniously when they leave the barrel
        if(stateMachine.GetComponent<Player>().previousPosition == new Vector3(0, 0, 0))
        {
            stateMachine.GetComponent<Player>().previousPosition = new Vector3(player.transform.position.x, player.transform.position.y + 0.2f, player.transform.position.z);
        }
        //Put them in the barrel
        player.transform.position = player.GetComponent<Player>().selectedHideable;
        if (!stateMachine.GetComponent<Player>().hiding)
        {
            stateMachine.GetComponent<Player>().hiding = true;
        }
        player.consumingStamina = false;
        //Change player animation
    }
}
