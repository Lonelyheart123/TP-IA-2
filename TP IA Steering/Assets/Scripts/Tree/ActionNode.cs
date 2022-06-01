using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actionnode : INode
{
    Action _action;
    public Actionnode(Action action)
    {
        _action = action;
    }

    public void execute()
    {

    }
}