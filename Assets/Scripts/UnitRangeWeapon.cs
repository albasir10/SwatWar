using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRangeWeapon : Unit
{


    protected override void Update()
    {
        base.Update();
        CheckCanShoot();
    }

    public void CheckCanShoot()
    {
        if (currentTarget != null && stateMachine.currentState.GetType() == typeof(StateIdleRifle))
        {

            stateMachine.ChangeState(new StateAttack());


        }
    }

}
