using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Human")]
public class GainCrop : Action
{
    [SerializeField] private SharedHuman Human;
    [SerializeField] private SharedLand Land;

    public override TaskStatus OnUpdate()
    {
        if (Human.Value != null)
        {
            (Human.Value as Farm).GainCrop(Land.Value);
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
