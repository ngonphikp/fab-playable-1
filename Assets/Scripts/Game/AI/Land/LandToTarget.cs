using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Land")]
public class LandToTarget : Action
{
    [SerializeField] private SharedLand Land;
    [SerializeField] private SharedTransform Target;

    public override TaskStatus OnUpdate()
    {
        if (Land.Value)
        {
            Target.Value = Land.Value.transform;
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
