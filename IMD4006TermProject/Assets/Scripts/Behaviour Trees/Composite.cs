using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Parent class to Sequence and Selector
//Tells the branch of the tree how it should run
public abstract class Composite : BTNode
{
    protected int CurrentChildIndex = 0;

    protected Composite(string displayName, List<BTNode> childNodes) : base(displayName, childNodes)
    {
        Name = displayName;

        children.AddRange(childNodes);
    }
}
