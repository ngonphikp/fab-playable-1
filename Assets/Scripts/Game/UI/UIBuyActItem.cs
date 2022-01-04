using NPS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyActItem : MonoBehaviour
{
    [SerializeField] private Image m_ImgIcon;
    [SerializeField] private TextMeshProUGUI m_TxtCost;

    private ActType actType;

    private void OnEnable()
    {
        m_TxtCost.text = "=" + MathHelper.ScoreShow(DataManager.Data.Act.GetCost(actType));
    }

    public void Set(ActType actType)
    {
        this.actType = actType;

        m_TxtCost.text = "=" + MathHelper.ScoreShow(DataManager.Data.Act.GetCost(actType));
    }
}
