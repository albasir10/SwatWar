using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateIdleRifle : State
{
    public override void StartState(StateMachine stateMachine)
    {
        base.StartState(stateMachine);

        stateMachine.unit.animator.SetBool("IdleRifle", true);
    }

    public override void UpdateState()
    {

    }

    public override void EndState()
    {
        stateMachine.unit.animator.SetBool("IdleRifle", false);
    }
}
