using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Human")]
public class GetLands : Action
{
    [SerializeField] private SharedHuman Human;
    [SerializeField] private SharedLands Lands;

    public override TaskStatus OnUpdate()
    {
        var lands = Human.Value.GetLands();
        if (lands)
        {
            Lands.Value = lands;
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
