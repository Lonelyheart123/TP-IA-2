using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States<T> : IStates<T>
{

    Dictionary<T, IStates<T>> _transitions = new Dictionary<T, IStates<T>>();
    public float shootTime;
    public FSM<T> fsm;
    public T input;

    public FSM<T> StateMachine
    {
        get
        {
            return fsm;
        }

        set
        {
            fsm = value;
        }
    }

    public IStates<T> GetTransition(T input)
    {
        if (_transitions.ContainsKey(input))
        {
            return _transitions[input];
        }
        else
        {
            return null;
        }
    }
    public void AddTransition(T input, IStates<T> state)
    {
        _transitions[input] = state;
    }
    public void RemoveTransition(T input)
    {
        if (_transitions.ContainsKey(input))
        {
            _transitions.Remove(input);
        }
    }
    public void RemoveTransition(IStates<T> state)
    {
        foreach (var item in _transitions)
        {
            if (item.Value == state)
            {
                _transitions.Remove(item.Key);
            }
        }
    }
    public virtual void Init()
    {

    }
    public virtual void Execute()
    {

    }
    public virtual void Exit()
    {

    }
}
