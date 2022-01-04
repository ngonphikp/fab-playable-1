using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Lands")]
public class HasStatusLands : Conditional
{
    [SerializeField] private SharedLands Lands;
    [SerializeField] private List<Lands.Status> status = new List<Lands.Status>();

    public override TaskStatus OnUpdate()
    {
        if (Lands.Value)
        {
            for (int i = 0; i < status.Count; i++)
            {
                if (Lands.Value.GetStatus == status[i])
                {
                    return TaskStatus.Success;
                }
            }
        }

        return TaskStatus.Failure;
    }
}
