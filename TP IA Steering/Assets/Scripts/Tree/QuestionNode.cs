using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestionNode : INode
{
    Func<bool> _question;
    INode _nT;
    INode _nF;
    public QuestionNode(Func<bool> newQuestion, INode nT, INode nF)
    {
        _question = newQuestion;
        _nT = nT;
        _nF = nF;
    }
    public void execute()
    {
        if (_question())
        {
            _nT.execute();
        }
        else
        {
            _nF.execute();
        }
    }
}
