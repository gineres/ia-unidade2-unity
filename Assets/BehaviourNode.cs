using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState {
    RUNNING,
    SUCCESS,
    FAILURE
}

public class BehaviourNode
{
    protected NodeState state;
    public BehaviourNode parent;
    protected List<BehaviourNode> children = new List<BehaviourNode>();

    public BehaviourNode(){
        parent = null;
    }

    public BehaviourNode(List<BehaviourNode> children){
        foreach (BehaviourNode child in children){
            _Attach(child);
        }
    }

    private void _Attach(BehaviourNode node){
        node.parent = this;
        children.Add(node);
    }

    public virtual NodeState Evaluate() => NodeState.FAILURE; 
}
