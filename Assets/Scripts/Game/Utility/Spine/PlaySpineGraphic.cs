using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;

public class PlaySpineGraphic : MonoBehaviour {
    [SerializeField] private SkeletonGraphic m_SkeletonAnimation;
    [SerializeField, SpineAnimation] private string m_Animation;
    [SerializeField] private bool m_PlayOnEnable;
    [SerializeField] private bool m_Loop;
    [SerializeField] private bool m_EventOnlyThisAnimation;
    [SerializeField] private UnityEvent<string, string> m_OnEvent;
    [SerializeField] private UnityEvent<string> m_OnComplete;

    public void Play() {
        m_SkeletonAnimation.ChangeAnimation(m_Animation, m_Loop);
    }

    private void OnEnable() {
        if (m_PlayOnEnable) {
            m_SkeletonAnimation.ChangeAnimation(m_Animation, m_Loop);
        }
    }

    private void Start() {
        m_SkeletonAnimation.AnimationState.Event += OnEventHandler;
        m_SkeletonAnimation.AnimationState.Complete += OnCompleteHandler;
    }

    private void OnDestroy() {
        m_SkeletonAnimation.AnimationState.Event -= OnEventHandler;
        m_SkeletonAnimation.AnimationState.Complete -= OnCompleteHandler;
    }

    private void OnEventHandler(Spine.TrackEntry trackEntry, Spine.Event e) {
        if (m_EventOnlyThisAnimation) {
            if (trackEntry.Animation.Name.Equals(m_Animation)) {
                m_OnEvent?.Invoke(trackEntry.Animation.Name, e.Data.Name);
            }

            return;
        }

        m_OnEvent?.Invoke(trackEntry.Animation.Name, e.Data.Name);
    }

    private void OnCompleteHandler(Spine.TrackEntry trackEntry) {
        if (m_EventOnlyThisAnimation) {
            if (trackEntry.Animation.Name.Equals(m_Animation)) {
                m_OnComplete?.Invoke(trackEntry.Animation.Name);
            }

            return;
        }

        m_OnComplete?.Invoke(trackEntry.Animation.Name);
    }
}