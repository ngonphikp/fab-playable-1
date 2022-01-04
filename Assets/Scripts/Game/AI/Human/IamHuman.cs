using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[TaskCategory("Human")]
public class IamHuman : Action
{
    [SerializeField] private Human human;
    [SerializeField] private SharedHuman Human;

    public override TaskStatus OnUpdate()
    {
        if (human)
        {
            Human.Value = human;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
