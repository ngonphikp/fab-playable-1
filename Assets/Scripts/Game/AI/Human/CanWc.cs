using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Human")]
public class CanWc : Action
{
    [SerializeField] private SharedHuman Human;

    public override TaskStatus OnUpdate()
    {
        if (Human.Value)
        {
            if (Random.Range(0f, 1f) <= Human.Value.Entity.RatioWc)
            {
                Human.Value.Data.SetWc(true);
                return TaskStatus.Success;
            }            
        }
        return TaskStatus.Failure;
    }
}
