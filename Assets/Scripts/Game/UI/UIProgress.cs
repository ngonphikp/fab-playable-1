using MEC;
using NPS;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIProgress : MonoBehaviour
{
    public Transform TranCrop => m_ImgIcon.transform;

    [SerializeField] private Image m_ImgIcon;
    [SerializeField] private Image m_ImgProgress;
    [SerializeField] private TextMeshProUGUI m_TxtProgress;

    private CoroutineHandle handle;

    public void Set(ActType type)
    {
        m_ImgIcon.sprite = ResourceManager.S.LoadSprite("Crops", type.ToString());
        m_ImgIcon.SetNativeSize();
    }

#if UNITY_EDITOR
    public void ToolSet(ActType type)
    {
        m_ImgIcon.sprite = Resources.Load<Sprite>("Textures/Crops/" + type.ToString());
        m_ImgIcon.SetNativeSize();
    }
#endif

    public void Set(int count, int max)
    {
        if (m_ImgProgress == null) return;
        string content = MathHelper.ScoreShow(count) + "/" + MathHelper.ScoreShow(max);
        float amount = count * 1.0f / max;

        m_ImgProgress.fillAmount = amount;
        m_TxtProgress.text = content;
    }

    public void Set(float amount)
    {
        if (m_ImgProgress == null) return;
        m_ImgProgress.fillAmount = amount;
    }

    private void OnDisable()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
    }

    private void OnDestroy()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
    }

    public void CDT(float sumTime, Action callback = null)
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
        Timing.RunCoroutine(_CDT(sumTime, callback));
    }

    private IEnumerator<float> _CDT(float sumTime, Action callback = null)
    {
        float time = 0;
        while (time < sumTime)
        {
            time += 0.1f;
            m_ImgProgress.fillAmount = time / sumTime;
            yield return Timing.WaitForSeconds(0.1f);
        }

        callback?.Invoke();
    }
}
