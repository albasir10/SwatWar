using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateInjury : State
{
    public override void StartState(StateMachine stateMachine)
    {
        base.StartState(stateMachine);

        stateMachine.unit.animator.SetBool("Injury", true);
    }

    public override void UpdateState()
    {

    }

    public override void EndState()
    {
        stateMachine.unit.animator.SetBool("Injury", false);
    }
}
