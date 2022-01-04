using I2.Loc;
using NPS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpgrade : MonoBehaviour
{
    public Transform TranCoin => m_TranCoin;

    [SerializeField] private GameObject m_ObjCoin;
    [SerializeField] private GameObject m_ObjAds;

    [SerializeField] private TextMeshProUGUI m_TxtCost;
    [SerializeField] private TextMeshProUGUI m_TxtLevel;

    [SerializeField] private Transform m_TranCoin;

    int level = -1;

    private void Start()
    {
        LocalizationManager.OnLocalizeEvent += OnLocalize;
    }

    private void OnLocalize()
    {
        m_TxtLevel.text = level ==-1 ? LocalizationManager.GetTranslation("Max") : LocalizationManager.GetTranslation("Level") + " " + (level + 1);
    }

    private void OnDestroy()
    {
        LocalizationManager.OnLocalizeEvent -= OnLocalize;
    }

    public void SetLevel(int level)
    {
        this.level = level;
        m_TxtLevel.text = LocalizationManager.GetTranslation("Level") + " " + (level + 1);
    }

    public void SetMaxLevel()
    {
        this.level = -1;
        m_TxtLevel.text = LocalizationManager.GetTranslation("Max");
        m_TxtCost.text = "???";
        m_ObjAds.SetActive(false);
        m_ObjCoin.SetActive(true);
    }

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
