using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Tree
{
    Pathfinding pathfinding;
    private float moveSpeed = 1f;

    protected override BehaviourNode SetupTree(){
        //BehaviourNode root = new EnemyTaskWait();
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
    
    /*

    void Update()
    {
        /* LOGICA DE SEGUIR O JOGADOR
        if (pathfinding.foundPath != null)
        {
            if (pathfinding.foundPath.Count > 0)
            {
                Vector3 direction = pathfinding.foundPath[0].WorldPosition - transform.position;
                transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
            }
        }*/

        /*
        if (pathfinding.foundPath != null)
        {
            if (pathfinding.foundPath.Count > 20)
            {
                
            }
        }
    }*/
}
