using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractCircle : MonoBehaviour
{
    [SerializeField] private GameObject m_Graphic;

    private SkeletonAnimation sa;
    private void Awake()
    {
        sa = m_Graphic.GetComponent<SkeletonAnimation>();
        sa.Initialize(false);
    }

    public void Motion()
    {
        sa.AnimationState.SetAnimation(0, "Signal", true);
    }

    public void UnMotion()
    {
        sa.AnimationState.SetAnimation(0, "Idle", true);
    }
}
