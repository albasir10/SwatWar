using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public StateMachine stateMachine;
    public virtual void StartState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void UpdateState()
    {

    }
    
    public virtual void EndState()
    {

    }
}
