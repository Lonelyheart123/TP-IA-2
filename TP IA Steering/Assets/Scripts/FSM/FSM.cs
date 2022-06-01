using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    IStates<T> _current;
    //public FSM(IStates<T> init)
    //{
    //    SetInit(init);
    //}
    public void SetInit(IStates<T> init)
    {
        _current = init;
        _current.StateMachine = this;
        _current.Init();
    }
    public void OnUpdate()
    {
        if (_current != null)
        {
            _current.Execute();
        }           
    }
    public void Transition(T input)
    {
        var newState = _current.GetTransition(input);
        if (newState != null)
        {
            newState.StateMachine = this;
            _current.Exit();
            _current = newState;
            _current.Init();
        }
    }
}