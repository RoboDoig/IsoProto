using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GoapAction
{
    public static List<GoapAction> availableActions = new List<GoapAction>();

    public string name;
    public List<WorldItem> preconditions;
    public List<WorldItem> effects;

    public GoapAction(string _name)
    {
        name = _name;
        availableActions.Add(this);
    }

    public void DoAction()
    {

    }

}
