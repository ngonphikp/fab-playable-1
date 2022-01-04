using BehaviorDesigner.Runtime.Tasks;
using Spine.Unity;
using UnityEngine;

[TaskCategory("Common")]
public class SetSkeletonAnimation : Action
{
    [SerializeField] private SkeletonAnimation sa;
    [SerializeField] private string anim;
    [SerializeField] private bool loop;

    public override void OnStart()
    {
        base.OnStart();
        if (sa)
        {
            sa.Initialize(false);
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (sa)
        {            
            sa.AnimationState.SetAnimation(0, anim, loop);
            return TaskStatus.Success;
        }        

        return TaskStatus.Failure;
    }
}
