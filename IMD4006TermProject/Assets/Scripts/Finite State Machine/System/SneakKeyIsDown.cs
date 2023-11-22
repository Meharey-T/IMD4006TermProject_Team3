using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/SneakKeyIsDown")]
public class SneakKeyIsDown : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        TerrainState terraine = stateMachine.GetComponent<TerrainState>();
        if (Input.GetKeyDown("c"))
        {
           
            stateMachine.GetComponent<TerrainState>().currSpeed = stateMachine.GetComponent<TerrainState>().GetSneak();
            //play sneak sfx
            terraine.PlaySneakingSound();
            return true;
        }
      
        return false;
    }
}
