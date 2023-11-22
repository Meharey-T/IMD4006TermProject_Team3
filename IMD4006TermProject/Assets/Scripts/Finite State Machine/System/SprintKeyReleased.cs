using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/SprintKeyReleased")]
public class SprintKeyReleased : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
          
            return true;
        }
        return false;
    }
}
