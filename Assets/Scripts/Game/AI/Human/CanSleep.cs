using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Human")]
public class CanSleep : Action
{
    [SerializeField] private SharedHuman Human;

    public override TaskStatus OnUpdate()
    {
        if (Human.Value)
        {
            if (Random.Range(0f, 1f) <= Human.Value.Entity.RatioSleep)
            {
                Human.Value.Data.SetSleep(true);
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }
}
