using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Land")]
public class SetStatusLand : Action
{
    [SerializeField] private SharedLand Land;
    [SerializeField] private Land.Status status;

    public override TaskStatus OnUpdate()
    {
        if (Land.Value)
        {
            Land.Value.SetStatus(status);
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
