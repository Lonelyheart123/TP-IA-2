using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStates<T>
{
    /// <summary>
    /// AWAKE
    /// </summary>
    void Init();
    /// <summary>
    /// EXECUTE / UPDATE
    /// </summary>
    void Execute();
    /// <summary>
    /// SLEEP
    /// </summary>
    void Exit();
    void AddTransition(T input, IStates<T> state);
    void RemoveTransition(T input);
    void RemoveTransition(IStates<T> state);
    IStates<T> GetTransition(T input);
    FSM<T> StateMachine { get; set; }
}
