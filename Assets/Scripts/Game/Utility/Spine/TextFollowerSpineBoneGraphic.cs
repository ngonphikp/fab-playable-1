using System;
using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using TMPro;
using UnityEngine;

public class TextFollowerSpineBoneGraphic : MonoBehaviour {
    public SkeletonGraphic SkeletonGraphic;

    [SpineBone(dataField: "SkeletonGraphic")]
    public string BoneName;

    [SpineBone(dataField: "SkeletonGraphic")]
    public string ScaleBoneName;
    
    [SpineSlot(dataField: "SkeletonGraphic")]
    public string SlotName;

    public bool followPosition = true;
    public bool followRotation = true;
    public bool followLocalScale = true;
    public bool followColor = true;

    private Bone m_Bone;
    private Bone m_ScaleBone;
    private Slot m_Slot;

    private bool m_Valid;
    private Transform m_SkeletonTransform;
    private RectTransform m_SelfTransform;
    private TextMeshProUGUI m_Text;
    private float m_ScaleFactor;

#if UNITY_EDITOR
    private void OnValidate() {
        if (SkeletonGraphic == null) {
            SkeletonGraphic = GetComponentInParent<SkeletonGraphic>();
        }

        if (m_Text == null) {
            m_Text = GetComponent<TextMeshProUGUI>();
        }
    }
#endif

    private void Awake() {
        if (m_Text == null) {
            m_Text = GetComponent<TextMeshProUGUI>();
        }

        if (SkeletonGraphic == null) {
            SkeletonGraphic = GetComponentInParent<SkeletonGraphic>();
        }

        m_Valid = SkeletonGraphic != null && SkeletonGraphic.IsValid;
        if (!m_Valid) return;

        m_SkeletonTransform = SkeletonGraphic.transform;
        m_SelfTransform = transform as RectTransform;

        if (!string.IsNullOrEmpty(BoneName)) {
            m_Bone = SkeletonGraphic.Skeleton.FindBone(BoneName);
        }

        if (!string.IsNullOrEmpty(BoneName)) {
            m_ScaleBone = SkeletonGraphic.Skeleton.FindBone(ScaleBoneName);
        }
        
        if (!string.IsNullOrEmpty(SlotName)) {
            m_Slot = SkeletonGraphic.Skeleton.FindSlot(SlotName);
        }

        var canvas = SkeletonGraphic.canvas;
        if (canvas == null) canvas = SkeletonGraphic.GetComponentInParent<Canvas>();
        m_ScaleFactor = canvas != null ? canvas.referencePixelsPerUnit : 100.0f;

#if UNITY_EDITOR
        if (Application.isEditor) {
            LateUpdate();
        }
#endif
    }

    private void LateUpdate() {
        if (!m_Valid) return;

        if (followPosition) {
            m_SelfTransform.position = m_Bone.GetWorldPosition(m_SkeletonTransform, m_ScaleFactor);
        }

        if (followRotation) {
            m_SelfTransform.rotation = m_Bone.GetQuaternion();
        }

        if (followLocalScale) {
            m_SelfTransform.localScale = new Vector3(m_ScaleBone.ScaleX, m_ScaleBone.ScaleY, 1f);
        }

        if (followColor) {
            var color = m_Text.color;
            color.a = m_Slot.GetColor().a;
            m_Text.color = color;
        }
    }
}