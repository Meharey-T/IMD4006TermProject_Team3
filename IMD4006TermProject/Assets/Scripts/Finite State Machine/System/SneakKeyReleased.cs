using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/SneakKeyReleased")]
public class SneakKeyReleased : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            return true;
        }
        return false;
    }
}
