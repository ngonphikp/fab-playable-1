using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Human")]
public class SetSleep : Action
{
    [SerializeField] private SharedHuman Human;
    [SerializeField] private bool value = false;

    public override TaskStatus OnUpdate()
    {
        if (Human.Value)
        {
            Human.Value.Data.SetSleep(value);
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
