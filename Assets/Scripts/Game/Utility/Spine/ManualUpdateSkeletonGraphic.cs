using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class ManualUpdateSkeletonGraphic : MonoBehaviour {
    public SkeletonGraphic skeletonGraphic;

    [Range(1 / 60f, 1f / 8f)] // slider from 60fps to 8fps
    public float timeInterval = 1f / 30f; // 30fps

    private float m_DeltaTime;

#if UNITY_EDITOR
    private void OnValidate() {
        if (skeletonGraphic == null) {
            skeletonGraphic = GetComponent<SkeletonGraphic>();
        }
    }
#endif

    private void Start() {
        skeletonGraphic.Initialize(false);
        skeletonGraphic.enabled = false;
        ManualUpdate();
    }

    private void Update() {
        m_DeltaTime += Time.deltaTime;
        if (m_DeltaTime >= timeInterval) {
            ManualUpdate();
        }
    }

    private void ManualUpdate() {
        skeletonGraphic.Update(m_DeltaTime);
        skeletonGraphic.LateUpdate();
        m_DeltaTime -= timeInterval; //deltaTime = deltaTime % timeInterval; // optional time accuracy.
    }
}
