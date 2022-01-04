using System;
using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using Event = Spine.Event;

public class PlaySpineGraphicOnEnable : MonoBehaviour {
    [SerializeField] private SkeletonGraphic m_SkeletonAnimation;
    [SerializeField, SpineAnimation] private string m_Animation;
    [SerializeField] private UnityEvent<Spine.TrackEntry, Spine.Event> m_OnEvent;
    [SerializeField] private UnityEvent<Spine.TrackEntry> m_OnComplete;

    public UnityEvent<Spine.TrackEntry, Spine.Event> OnEvent => m_OnEvent;
    public UnityEvent<Spine.TrackEntry> OnComplete => m_OnComplete;

    private void OnEnable() {
        m_SkeletonAnimation.ChangeAnimation(m_Animation, false);
    }

    private void Start() {
        m_SkeletonAnimation.AnimationState.Event += OnEventHandler;
        m_SkeletonAnimation.AnimationState.Complete += OnCompleteHandler;
    }

    private void OnDestroy() {
        m_SkeletonAnimation.AnimationState.Event -= OnEventHandler;
        m_SkeletonAnimation.AnimationState.Complete -= OnCompleteHandler;
    }

    private void OnEventHandler(TrackEntry trackEntry, Event e) {
        m_OnEvent?.Invoke(trackEntry, e);
    }

    private void OnCompleteHandler(TrackEntry trackEntry) {
        m_OnComplete?.Invoke(trackEntry);
    }
}