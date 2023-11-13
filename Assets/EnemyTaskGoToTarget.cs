using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTaskGoToTarget : BehaviourNode
{
    Transform transform;
    Pathfinding pathfinding;
    float radiusLimit = 0.1f;
    private float moveSpeed = 1.5f;
    private float arrivalRadius = .5f;
    private float maxAcceleration = 10f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 playerPosition;

    public EnemyTaskGoToTarget(Transform transform, Pathfinding pathfinding)
    {
        this.pathfinding = pathfinding;
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        if (pathfinding.foundPath.Count > 0)
        {
            ArrivalToTarget(pathfinding.foundPath[0].WorldPosition);
        } else
        {
            ArrivalToPlayer();
        }

        state = NodeState.RUNNING;
        return state;
    }

    void ArrivalToTarget(Vector3 target){
        Vector3 targetPosition = target;
        playerPosition = targetPosition;
        Vector3 toTarget = targetPosition - transform.position;
        float distance = toTarget.magnitude;

        float slowingRadius = arrivalRadius;
        float rampedSpeed = moveSpeed * (distance / slowingRadius);
        float clippedSpeed = Mathf.Min(rampedSpeed, moveSpeed);

        Vector3 desiredVelocity = (clippedSpeed / distance) * toTarget;
        Vector3 steering = desiredVelocity - velocity;
        Vector3 acceleration = Vector3.ClampMagnitude(steering, maxAcceleration);

        velocity += acceleration * Time.deltaTime;

        transform.Translate(velocity.normalized * Time.deltaTime);
    }

    void ArrivalToPlayer(){
        Vector3 targetPosition = playerPosition;
        Vector3 toTarget = targetPosition - transform.position;
        float distance = toTarget.magnitude;

        if (distance > radiusLimit)
        {
            float slowingRadius = arrivalRadius;
            float rampedSpeed = moveSpeed * (distance / slowingRadius);
            float clippedSpeed = Mathf.Min(rampedSpeed, moveSpeed);

            Vector3 desiredVelocity = (clippedSpeed / distance) * toTarget;
            Vector3 steering = desiredVelocity - velocity;
            Vector3 acceleration = Vector3.ClampMagnitude(steering, maxAcceleration);

            velocity += acceleration * Time.deltaTime;
        } else
        {
            velocity = Vector3.zero;
        }
        transform.Translate(velocity.normalized * Time.deltaTime);
    }
}
