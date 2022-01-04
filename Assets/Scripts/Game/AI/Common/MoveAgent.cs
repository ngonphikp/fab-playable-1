using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Common")]
public class MoveAgent : Action
{
    [SerializeField] protected SharedTransform Target;
    [SerializeField] private bool isRandom = false;
    [SerializeField] private float minDistane = 0.1f;
    [SerializeField] private float maxDistane = 1f;

    [SerializeField] private bool isDirection = true;
    [SerializeField] private GameObject self = null;

    private float distance = 0.1f;
    protected float speed = 1.0f;

    //protected NavMeshAgent agent;

    public override void OnStart()
    {
        base.OnStart();

        distance = isRandom ? Random.Range(minDistane, maxDistane) : minDistane;

        if (self == null) self = this.gameObject;
    }

    public override TaskStatus OnUpdate()
    {
        if (Target.Value == null)
        {
            return TaskStatus.Failure;
        }

        float dis = Mathf.Abs(Vector3.SqrMagnitude(transform.position - Target.Value.transform.position));

        //if (dis <= distance)
        //{
        //    agent.ResetPath();
        //    return TaskStatus.Success;
        //}

        //if (isDirection)
        //{
        //    Direction direction = agent.desiredVelocity.normalized.x > 0 ? Direction.Right : Direction.Left;
        //    SetDirection(direction);
        //}
        //agent.SetDestination(Target.Value.position);

        if (dis <= distance)
        {
            return TaskStatus.Success;
        }

        float x = transform.position.x;
        transform.position = Vector3.MoveTowards(transform.position, Target.Value.transform.position, speed * Time.deltaTime);

        if (isDirection)
        {
            Direction direction = x < transform.position.x ? Direction.Right : Direction.Left;
            SetDirection(direction);
        }

        return TaskStatus.Running;
    }

    protected virtual void SetDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                self.transform.localScale = new Vector3(-1, 1, 1);
                break;
            case Direction.Right:
                self.transform.localScale = new Vector3(1, 1, 1);
                break;
            default:
                break;
        }
    }
}
