using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine
{
    public State currentState;
    public Unit unit;

    public StateMachine(Unit unit, State startState)
    {
        this.unit = unit;
        currentState = startState;
        currentState.StartState(this);

    }

    public void ChangeState(State newState)
    {
        currentState.EndState();
        currentState = newState;
        currentState.StartState(this);
    }

}
