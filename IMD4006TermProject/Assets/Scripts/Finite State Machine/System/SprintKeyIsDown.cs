using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/SprintKeyIsDown")]
public class SprintKeyIsDown : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            return true;
        }
        return false;
    }
}
