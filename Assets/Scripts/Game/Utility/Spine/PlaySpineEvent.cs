using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;

public class PlaySpineEvent : MonoBehaviour {
    [SerializeField] [SpineEvent] private string m_EventName;
    [SerializeField] private UnityEvent m_OnEventListener;

    public UnityEvent OnEventListener => m_OnEventListener;

    public void OnSpineEventHandler(Spine.TrackEntry trackEntry, Spine.Event e) {
        if (e.Data.Name.Equals(m_EventName)) {
            m_OnEventListener?.Invoke();
        }
    }
}