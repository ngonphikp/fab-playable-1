using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Store")]
public class StoreToTarget : Action
{
    [SerializeField] private SharedStore Store;
    [SerializeField] private SharedTransform Target;

    public override TaskStatus OnUpdate()
    {
        if (Store.Value)
        {
            Target.Value = Store.Value.GetDoor();
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
