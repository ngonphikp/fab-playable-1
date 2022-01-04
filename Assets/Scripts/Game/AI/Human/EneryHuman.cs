using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EneryHuman : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private ProgressBarPro m_Progress;
    [SerializeField] private Image m_Enery;
    [SerializeField] private List<EnerySp> m_EnerySps;

    private void OnEnable()
    {
        m_Progress.AddListener(ChangeHandler);
    }

    private void ChangeHandler(float value)
    {
        int idx = 0;
        for (int i = 0; i < m_EnerySps.Count; i++)
        {
            if (value < m_EnerySps[i].value) idx = i;
        }

        m_Enery.sprite = m_EnerySps[idx].sprite;
    }

    private void OnDisable()
    {
        m_Progress.RemoveListener(ChangeHandler);
    }

    public void SetTime(float time)
    {
        m_Progress.animTime = time;
    }

    public void SetAmount(float amount)
    {
        m_Progress.SetValue(amount);        
    }

    [System.Serializable]
    private class EnerySp
    {
        public Sprite sprite;
        public float value;
    }
}
