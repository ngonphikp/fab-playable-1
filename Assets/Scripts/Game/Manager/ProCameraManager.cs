using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProCameraManager : MonoBehaviour
{
    public static ProCameraManager S;

    private ProCamera2D m_Camera;

    private void Awake()
    {
        if (!S) S = this;

        m_Camera = FindObjectOfType<ProCamera2D>();
    }

    private void Start()
    {
        
    }

    public void AddFollowTarget(Transform target, float targetInfluenceH = 1, float targetInfluenceV = 1, float duration = 0, Vector2 targetOffset = default(Vector2))
    {
        if (m_Camera == null) return;
        m_Camera.AddCameraTarget(target, targetInfluenceH, targetInfluenceV, duration, targetOffset);
    }

    public void RemoveFollowTarget(Transform target, float duration = 0)
    {
        if (m_Camera == null) return;
        m_Camera.RemoveCameraTarget(target, duration);
    }

    public void FollowTime(Transform target, float timeGo, float timeBack, float timeStay = 0f)
    {
        StartCoroutine(_FollowTime(target, timeGo, timeBack, timeStay));
    }

    private IEnumerator _FollowTime(Transform target, float timeGo, float timeBack, float timeStay = 0f)
    {
        var oldTarget = new List<CameraTarget>();
        foreach (var item in m_Camera.CameraTargets)
        {
            oldTarget.Add(item);
        }

        m_Camera.RemoveAllCameraTargets();
        AddFollowTarget(target, 1, 1, timeGo);

        yield return new WaitForSeconds(timeGo + timeStay);

        RemoveFollowTarget(target, timeBack);
        yield return new WaitForSeconds(timeBack);
        m_Camera.CameraTargets = oldTarget;
    }
}
