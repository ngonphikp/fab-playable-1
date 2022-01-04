using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class PlayClipSpineGraphic : MonoBehaviour {
    [SerializeField] private SkeletonGraphic m_SkeletonAnimation;
    [SerializeField, SpineAnimation] private string m_Animation;
    [SerializeField] private bool m_Loop;

    public void Play() {
        m_SkeletonAnimation.ChangeAnimation(m_Animation, m_Loop);
    }
}