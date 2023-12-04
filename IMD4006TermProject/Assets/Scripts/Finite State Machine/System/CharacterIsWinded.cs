using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/CharacterIsWinded")]
public class CharacterIsWinded : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        if (stateMachine.GetComponent<PlayerMovement>().winded)
        {
            stateMachine.GetComponent<PlayerMovement>().StartCoroutine(stateMachine.GetComponent<PlayerMovement>().CatchBreath());
            return true;
        }
        return false;
    }
}
