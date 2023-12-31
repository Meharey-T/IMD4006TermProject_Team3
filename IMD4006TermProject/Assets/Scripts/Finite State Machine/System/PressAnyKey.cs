using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/PressAnyKey")]
public class PressAnyKey : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        if (Input.anyKeyDown)
        {
            return true;
        }
        TerrainState terraine = stateMachine.GetComponent<TerrainState>();
        terraine.PlayIdleSound();
        return false;
    }
}
