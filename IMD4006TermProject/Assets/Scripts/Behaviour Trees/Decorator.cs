using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

//Can be used to modify a node without hard-coding the change into the node itself
//Includes inverter and timer classes
public abstract class Decorator : BTNode
{
    public Decorator(string displayName, List<BTNode> node) : base(displayName, node)
    {
        Name = displayName;
        children.AddRange(node);
    }
}
