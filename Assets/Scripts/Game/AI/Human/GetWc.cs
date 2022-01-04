using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Human")]
public class GetWc : Action
{
    [SerializeField] private SharedHuman Human;
    [SerializeField] private SharedTransform Target;

    public override TaskStatus OnUpdate()
    {
        if (Human.Value)
        {
            Target.Value = Human.Value.GetWc();
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
