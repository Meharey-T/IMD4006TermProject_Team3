using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/SneakKeyIsDown")]
public class SneakKeyIsDown : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            return true;
        }
        return false;
    }
}
