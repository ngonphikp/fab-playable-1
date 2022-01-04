using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;
using Spine.Unity;
using UnityEngine.Events;
using Event = UnityEngine.Event;

public class PlaySpineOnEnable : MonoBehaviour {
    [SerializeField, SpineAnimation] private string m_Animation;
    [SerializeField] private SkeletonAnimation m_SkeletonAnimation;
    [SerializeField] private UnityEvent<Spine.TrackEntry, Spine.Event> m_OnEvent;
    [SerializeField] private UnityEvent<Spine.TrackEntry> m_OnComplete;

    private void OnEnable() {
        m_SkeletonAnimation.ChangeAnimation(m_Animation, false, true);
    }

    private void Start() {
        m_SkeletonAnimation.AnimationState.Event += OnEventHandler;
        m_SkeletonAnimation.AnimationState.Complete += OnCompleteHandler;
    }

    private void OnDestroy() {
        m_SkeletonAnimation.AnimationState.Event -= OnEventHandler;
        m_SkeletonAnimation.AnimationState.Complete -= OnCompleteHandler;
    }

    private void OnEventHandler(TrackEntry trackEntry, Spine.Event e) {
        m_OnEvent?.Invoke(trackEntry, e);
    }

    private void OnCompleteHandler(TrackEntry trackEntry) {
        m_OnComplete?.Invoke(trackEntry);
    }
}