using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "FSM/Decisions/HideButtonIsDown")]
public class HideButtonIsDown : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        if (Input.GetKeyDown(KeyCode.E) || (Input.GetKeyDown(KeyCode.Mouse0)))
        {
            if(stateMachine.GetComponent<Player>().inRangeOfHideable)
                return true;
        }
        return false;
    }
}
