using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Human")]
public class DeregisterLand : Action
{
    [SerializeField] private SharedHuman Human;
    [SerializeField] private SharedLand Land;

    public override TaskStatus OnUpdate()
    {
        if (Land.Value != null && Human.Value != null)
        {
            Land.Value.Deregister(Human.Value);
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
