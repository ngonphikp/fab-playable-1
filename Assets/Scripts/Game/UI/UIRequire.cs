using NPS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRequire : MonoBehaviour
{
    public Transform TranStar => m_TranStar;

    [SerializeField] private TextMeshProUGUI m_TxtLevel;

    [SerializeField] private Transform m_TranStar;

    public void SetLevel(int level)
    {
        m_TxtLevel.text = (level + 1).ToString();
    }

    public void Motion()
    {
        this.transform.localScale = new Vector3(1.3f, 1.3f, 1);
    }

    public void UnMotion()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }
}
