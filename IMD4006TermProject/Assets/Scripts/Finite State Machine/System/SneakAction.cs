using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Sneak")]
public class SneakAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        stateMachine.GetComponent<TerrainState>().currSpeed = stateMachine.GetComponent<TerrainState>().GetSneak();
        /*
        if (stateMachine.GetComponent<TerrainState>().currSpeed == stateMachine.GetComponent<TerrainState>().GetSneak())
        { Debug.Log("I should be sneaking"); }
        */
        PlayerMovement player = stateMachine.GetComponent<PlayerMovement>();
        player.setModalSpeed(player.getSneakSpeed());
        player.turnSmoothTime = player.turnSmoothTimeSnappy;
        stateMachine.GetComponent<PlayerMovement>().currentSoundRadius = stateMachine.GetComponent<PlayerMovement>().sneakSoundRadius;
        player.gameObject.layer = 9;
        player.consumingStamina = false;
        //player.GetComponent<Player>().indicator.text = "";
        //Change player animation
        //Change player footstep sounds
    }
}
