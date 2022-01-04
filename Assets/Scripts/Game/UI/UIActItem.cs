using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActItem : MonoBehaviour
{
    public Transform TranIcon => m_ImgIcon.gameObject.transform;

    [SerializeField] private Image m_ImgIcon;
    [SerializeField] TextMeshProUGUI m_TxtAmount;

    private ActType type;

    public void Set(ActType type)
    {
        this.type = type;
        m_ImgIcon.sprite = ResourceManager.S.LoadSprite("Icons", type.ToString());

        this.gameObject.name = type.ToString();
    }

    public void Set(string content)
    {
        m_TxtAmount.text = content;
    }

    public void CheateCoin()
    {
#if UNITY_EDITOR || DEVELOPMENT
        DataManager.Save.Bag.IncreaseCurrency(CurrencyType.Coin, 100000);
#endif
    }
}
