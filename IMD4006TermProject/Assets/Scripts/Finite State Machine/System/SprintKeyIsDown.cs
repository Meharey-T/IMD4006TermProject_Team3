using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/SprintKeyIsDown")]
public class SprintKeyIsDown : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        TerrainState terraine = stateMachine.GetComponent<TerrainState>();
        if (Input.GetKeyDown(KeyCode.LeftShift) && !stateMachine.GetComponent<PlayerMovement>().winded)
        {
            

           terraine.PlayRunningSound();
            return true;
        }
        
        return false;
    }
}
