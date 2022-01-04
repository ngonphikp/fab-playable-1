using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Human")]
public class SetStatusHuman : Action
{
    [SerializeField] private SharedHuman Human;
    [SerializeField] private HumanStatus status;

    public override TaskStatus OnUpdate()
    {
        if (Human.Value)
        {
            Human.Value.SetStatus(status);
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
