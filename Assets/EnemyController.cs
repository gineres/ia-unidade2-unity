using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Tree
{
    Pathfinding pathfinding;

    protected override BehaviourNode SetupTree(){
        BehaviourNode root = new Selector(new List<BehaviourNode>{
            new Sequence(new List<BehaviourNode>{
                new CheckPlayerInRange(pathfinding),
                new EnemyTaskGoToTarget(gameObject.transform, pathfinding),
            }),
            new EnemyTaskWait(),
        });
        return root;
    }
    
    void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
    }
}
