using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Sprint")]
public class SprintAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        PlayerMovement player = stateMachine.GetComponent<PlayerMovement>();
        TerrainState terraine = stateMachine.GetComponent<TerrainState>();
        
        stateMachine.GetComponent<TerrainState>().currSpeed = stateMachine.GetComponent<TerrainState >().GetRun();
        /*
        if (stateMachine.GetComponent<TerrainState >().currSpeed == stateMachine.GetComponent <TerrainState>().GetRun())
        { Debug.Log("I should be running"); }
        */
        player.setModalSpeed(player.getSprintSpeed());
        player.turnSmoothTime = player.turnSmoothTimeSlow;
        stateMachine.GetComponent<PlayerMovement>().currentSoundRadius = stateMachine.GetComponent<PlayerMovement>().sprintSoundRadius;
        player.gameObject.layer = 9;
        player.consumingStamina = true;

        //Jump if we're running
        if (Input.GetButtonDown("Jump") && player.groundedPlayer && player.currentStamina >= 30)
        {
            player.rb.AddForce(Vector3.up * player.jumpAmount, ForceMode.Impulse);
            player.currentStamina -= 30;
        }
        //Change player animation
        //Change player footstep sounds
        //terraine.currSpeed = terraine.GetRun();
        //; = stateMachine.GetComponent<TerrainState>().GetRun() ;
    }
}
