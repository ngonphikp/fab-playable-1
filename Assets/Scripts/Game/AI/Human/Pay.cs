using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Human")]
public class Pay : Action
{
    [SerializeField] private SharedHuman Human;
    [SerializeField] private SharedStore Store;

    public override TaskStatus OnUpdate()
    {
        if (Human.Value != null)
        {
            Human.Value.Pay(Store.Value);
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
