using UnityEngine;
using Spine.Unity;

public class ManualUpdateSkeletonAnimation : MonoBehaviour {
    public SkeletonAnimation skeletonAnimation;

    [Range(1 / 60f, 1f / 8f)] // slider from 60fps to 8fps
    public float timeInterval = 1f / 30f; // 30fps

    private float m_DeltaTime;

#if UNITY_EDITOR
    private void OnValidate() {
        if (skeletonAnimation == null) {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
        }
    }
#endif

    private void Start() {
        skeletonAnimation.Initialize(false);
        skeletonAnimation.clearStateOnDisable = false;
        skeletonAnimation.enabled = false;
        ManualUpdate();
    }

    private void Update() {
        m_DeltaTime += Time.deltaTime;
        if (m_DeltaTime >= timeInterval) {
            ManualUpdate();
        }
    }

    private void ManualUpdate() {
        skeletonAnimation.Update(m_DeltaTime);
        skeletonAnimation.LateUpdate();
        m_DeltaTime = 0f;
        // m_DeltaTime -= timeInterval; //deltaTime = deltaTime % timeInterval; // optional time accuracy.
    }
}