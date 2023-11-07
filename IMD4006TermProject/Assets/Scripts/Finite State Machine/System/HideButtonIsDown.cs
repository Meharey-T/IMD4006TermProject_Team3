using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "FSM/Decisions/HideButtonIsDown")]
public class HideButtonIsDown : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(stateMachine.GetComponent<Player>().inRangeOfHideable)
                return true;
        }
        return false;
    }
}
