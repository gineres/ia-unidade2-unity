using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTaskWait : BehaviourNode
{
    public EnemyTaskWait(){
    }

    public override NodeState Evaluate(){
        Debug.Log("Esperando o jogador se aproximar...");
        state = NodeState.RUNNING;
        return state;
    }
}
