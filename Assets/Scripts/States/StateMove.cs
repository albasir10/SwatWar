using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class StateMove : State
{
    Vector3 point;
    Vector3 oldPosition;
    public StateMove(Vector3 point)
    {
        this.point = point;
    }

    public override void StartState(StateMachine stateMachine )
    {
        base.StartState(stateMachine);
        stateMachine.unit.animator.SetBool("Move", true);
        stateMachine.unit.agent.SetDestination(point);
    }

    public override void UpdateState()
    {
        
        if (Vector3.Distance(stateMachine.unit.transform.position, point) < 0.1f )
        {
            
            stateMachine.ChangeState( new StateIdleRifle() );
            
        }
    }


    public override void EndState()
    {
        stateMachine.unit.agent.SetDestination(stateMachine.unit.transform.position);
        stateMachine.unit.animator.SetBool("Move", false);
    }
}
