using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Human")]
public class MoveAgentHuman : MoveAgent
{
    [SerializeField] private SharedHuman Human;

    public override void OnStart()
    {
        //agent = Human.Value.GetComponent<NavMeshAgent>();

        speed = Human.Value.Data.Speed;
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        return base.OnUpdate();
    }

    protected override void SetDirection(Direction direction)
    {
        Human.Value.SetDirection(direction);
    }
}

