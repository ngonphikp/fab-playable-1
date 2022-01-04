using MEC;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class GroundLand : GroundAct
{
    [Header("Object")]
    [SerializeField] private Lands m_Lands;    

    [Header("UI")]
    [SerializeField] private UIProgress m_Progress;

    [Header("Properties")]
    [SerializeField] private GroundLandData landData = new GroundLandData();
    private GroundLandEntity landEntity;

    protected override void SetType(ActType type)
    {
        base.SetType(type);

        m_Progress.Set(actData.ActType);
    }

#if UNITY_EDITOR
    protected override void ToolSetType(ActType type)
    {
        base.ToolSetType(type);

        m_Progress.ToolSet(actData.ActType);
    }
#endif

    public override Lands GetLands()
    {
        return m_Lands;
    }

    public void ShowProgress(float sumTime, Action callback)
    {
        if (m_Progress.gameObject.activeSelf) return;

        m_Progress.gameObject.SetActive(true);
        m_Progress.CDT(sumTime, callback);
    }

    public void HideProgress()
    {
        m_Progress.gameObject.SetActive(false);
    }

    protected override void SetLevel0()
    {
        m_Lands.Set(this);
        IncreaseHuman();
    }

    protected override void LoadEntity()
    {
        landEntity = DataManager.Data.GroundLand.Dictionary[ActData.ActType];
        actEntity = landEntity;
    }

    protected override void UpgradeSucess(int level)
    {
        base.UpgradeSucess(level);
        m_Lands.UpgradeSuccess();
    }

    protected override void IncreaseQuantityAct(int value)
    {
        m_Lands.IncreaseQuatityCrop(value);
    }

    protected override void IncreaseLand(int value)
    {
        switch (value)
        {
            case 1:
                m_Lands.IncreaseRowTop();
                break;
            case 2:
                m_Lands.IncreaseColumnRight();
                break;
            case 3:
                m_Lands.IncreaseRowBottom();
                break;
            case 4:
                m_Lands.IncreaseColumnLeft();
                break;
        }
    }
}
