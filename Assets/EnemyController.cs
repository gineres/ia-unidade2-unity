using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Pathfinding pathfinding;
    [SerializeField] private float moveSpeed = 1f;
    void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
        //pathfinding.seeker = gameObject.transform;
    }

    void Update()
    {
        if (pathfinding.foundPath != null)
        {
            if (pathfinding.foundPath.Count > 0)
            {
                Vector3 direction = pathfinding.foundPath[0].WorldPosition - transform.position;
                transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
            }
        }
    }
}
