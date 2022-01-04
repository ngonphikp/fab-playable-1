using NPS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUnlock : MonoBehaviour
{
    public Transform TranCoin => m_TranCoin;

    [SerializeField] private GameObject m_ObjCoin;
    [SerializeField] private GameObject m_ObjAds;

    [SerializeField] private TextMeshProUGUI m_TxtCost;

    [SerializeField] private Transform m_TranCoin;

    public void SetCoin(int coin)
    {
        m_TxtCost.text = coin >= 0 ? MathHelper.ScoreShow((uint)coin) : "?";
        m_ObjAds.SetActive(coin == -1);
        m_ObjCoin.SetActive(coin != -1);
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
