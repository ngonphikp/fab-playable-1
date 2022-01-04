using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Common")]
public class StopAgent : Action
{
    protected NavMeshAgent agent;

    public override void OnStart()
    {
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        if (agent)
        {
            agent.SetDestination(this.transform.position);
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
