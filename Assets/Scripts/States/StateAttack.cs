using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : State
{
    Coroutine coroutine;
    public override void StartState(StateMachine stateMachine)
    {
        base.StartState(stateMachine);
        stateMachine.unit.animator.SetBool("AttackPosition", true);
        coroutine = stateMachine.unit.StartCoroutine(CooldownShoot());
    }

    public override void UpdateState()
    {
        if (stateMachine.unit.currentTarget != null && stateMachine.unit.angleVision.CheckTarget())
        {
            stateMachine.unit.transform.LookAt(stateMachine.unit.currentTarget.transform);
            //stateMachine.unit.body.transform.LookAt(stateMachine.unit.currentTarget.transform);
        }
        else
        {
            stateMachine.ChangeState( new StateIdleRifle () );
        }
    }

    IEnumerator CooldownShoot()
    {
        while (true) 
        {
            stateMachine.unit.weaponOption.Shoot(stateMachine.unit, stateMachine.unit.pointToShoot.transform);
            yield return new WaitForSeconds(stateMachine.unit.cooldownShoot);

        }
    }

    public override void EndState()
    {
        stateMachine.unit.StopCoroutine(coroutine);
        stateMachine.unit.currentTarget = null;
        stateMachine.unit.animator.SetBool("AttackPosition", false);
        stateMachine.unit.animator.SetBool("Shoot", false);
        
    }
}
