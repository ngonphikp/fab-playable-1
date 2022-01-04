using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[TaskCategory("Human")]
public class StopAgentHuman : StopAgent
{
    [SerializeField] private SharedHuman Human;

    public override void OnStart()
    {
        agent = Human.Value.GetComponent<NavMeshAgent>();
        base.OnStart();
    }

    public override TaskStatus OnUpdate()
    {
        return base.OnUpdate();
    }
}

