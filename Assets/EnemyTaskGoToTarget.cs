using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTaskGoToTarget : BehaviourNode
{
    Transform transform;
    Pathfinding pathfinding;
    private float moveSpeed = 1f;
    
    public EnemyTaskGoToTarget(Transform transform, Pathfinding pathfinding){
        this.pathfinding = pathfinding;
        this.transform = transform;
    }

    public override NodeState Evaluate(){
        if (pathfinding.foundPath.Count > 0)
        {
            Vector3 direction = pathfinding.foundPath[0].WorldPosition - transform.position;
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        }
        state = NodeState.RUNNING;
        return state;
    }
}
