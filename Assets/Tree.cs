using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tree : MonoBehaviour
{
    private BehaviourNode _root = null;

    protected void Start()
    {
        _root = SetupTree();
    }
    
    void Update()
    {
        if (_root != null)
        {
            _root.Evaluate();
        }
    }

    protected abstract BehaviourNode SetupTree();
}
