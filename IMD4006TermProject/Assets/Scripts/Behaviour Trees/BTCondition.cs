using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTCondition : BTNode
{
    public BTCondition(string name, List<BTNode> children) : base(name, children)
    {
        Name = name;
    }
}
