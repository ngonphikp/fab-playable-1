using MEC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TutorialFollow : MonoBehaviour
{
    private CoroutineHandle handle;
    private float oldSize;

    private void OnDestroy()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
    }

    public void SetTarget(Transform target)
    {
        ProCameraManager.S.AddFollowTarget(target, 1, 1, 0.5f);
    }

    public void UnTarget(Transform target)
    {
        ProCameraManager.S.RemoveFollowTarget(target);
    }

    public void SetSize(float size)
    {
        if (handle.IsValid)
        {
            Camera.main.orthographicSize = oldSize;
            Timing.KillCoroutines(handle);
        }

        handle = Timing.RunCoroutine(_SetSize(size));
        oldSize = size;
    }

    private IEnumerator<float> _SetSize(float size)
    {
        float step = (size - Camera.main.orthographicSize) / 50;

        for (int i = 0; i < 50; i++)
        {
            Camera.main.orthographicSize += step;
            yield return Timing.WaitForSeconds(0.01f);
        }

        Camera.main.orthographicSize = size;
    }
}
