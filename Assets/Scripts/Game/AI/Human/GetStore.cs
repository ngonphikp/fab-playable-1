using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Human")]
public class GetStore : Action
{
    [SerializeField] private SharedHuman Human;
    [SerializeField] private SharedStore Store;

    public override TaskStatus OnUpdate()
    {
        var store = Human.Value.GetStore();
        if (store)
        {
            Store.Value = store;
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
