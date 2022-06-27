using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionNode : INode
{
    Action _action;
    public ActionNode(Action action)
    {
        _action = action;
    }

    public void execute()
    {
        _action();
    }
}