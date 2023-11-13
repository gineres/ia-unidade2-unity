using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerInRange : BehaviourNode
{
    Pathfinding pathfinding;
    public CheckPlayerInRange(Pathfinding pathfinding){
        this.pathfinding = pathfinding;
    }
    
    public override NodeState Evaluate(){
        if (pathfinding.foundPath == null)
        {
            return NodeState.FAILURE;
        }
        if (pathfinding.foundPath.Count > 40)
        {
            return NodeState.FAILURE;
        } else
        {
            return NodeState.SUCCESS;
        }
    }
}
