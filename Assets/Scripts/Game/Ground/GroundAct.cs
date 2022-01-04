using DG.Tweening;
using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class GroundAct : Ground
{
    public GroundActSave ActSave => actSave;
    public GroundActData ActData => actData;

    [Header("Object")]
    [SerializeField] protected Transform m_PosHuman;
    [SerializeField] protected Signs m_Signs;

    //[Header("UI")]

    [Header("Properties")]
    [SerializeField] protected List<Transform> m_SleepPlaces;
    [SerializeField] private UpgradeGround upgrade;
    [SerializeField] protected GroundActData actData;
    protected GroundActSave actSave = new GroundActSave();
    protected GroundActEntity actEntity;

    protected HumanData humanTemp = new HumanData();
    protected List<Human> m_Humans = new List<Human>();
    protected CoroutineHandle handleUp;

#if UNITY_EDITOR
    private void OnValidate()
    {
        upgrade = this.GetComponent<UpgradeGround>();
    }
#endif

    public override void Set(GroupGround group)
    {
        base.Set(group);        

        upgrade.Init(Data, actSave, actData, actEntity);
        upgrade.m_OnUpgradeHandler.AddListener(UpgradeHandler);
        upgrade.m_OnUpgradeSuccess.AddListener(UpgradeSucess);

        SetType(actData.ActType);

        if (!Save.IsLock)
        {
            SetLevel0();

            for (int i = 0; i < actSave.Level; i++)
            {
                upgrade.Upgrade(i);
            }
        }
    }

    protected override void LoadDataPrivate()
    {
        base.LoadDataPrivate();

        actSave.Id = this.Id;
        actSave = DataManager.Save.Acts(actSave);

        var humanEntity = DataManager.Data.Human.Dictionary[actData.HumanType];
        humanTemp.SetSpeed(humanEntity.Speed);

        LoadEntity();
    }

    private void UpgradeHandler(int level)
    {
        var lv = actEntity.Levels[level] as LevelAct;

        switch (lv.Type)
        {
            case UpActType.IncreaseSpeedHuman:
                float speed = actEntity.Levels[level].Value;
                IncreSpeedHuman(speed);
                break;
            case UpActType.IncreaseQuantityCrop:
                int quantity = (int)actEntity.Levels[level].Value;
                IncreaseQuantityAct(quantity);
                break;
            case UpActType.IncreaseLand:
                int direction = (int)actEntity.Levels[level].Value;
                IncreaseLand(direction);
                break;
            case UpActType.IncreaseHuman:
                int human = (int)actEntity.Levels[level].Value;
                for (int i = 0; i < human; i++)
                {
                    IncreaseHuman();
                }
                break;
            case UpActType.IncreaseAnimal:
                int animal = (int)actEntity.Levels[level].Value;
                for (int i = 0; i < animal; i++)
                {
                    IncreaseAnimal();
                }
                break;
        }
    }

    protected virtual void UpgradeSucess(int level)
    {
        var lv = actEntity.Levels[level] as LevelAct;

        string path = lv.Type.ToString();

        switch (lv.Type)
        {
            case UpActType.IncreaseAnimal:
                path += ActData.ActType;
                break;
        }
        upgrade.ShowToast(path);
    }

#if UNITY_EDITOR
    public void ToolLoadDataAct()
    {
        ToolSetType(actData.ActType);
    }
#endif

    protected abstract void LoadEntity();

    public void OpenSuccess()
    {
        SetLevel0();
    }

    protected abstract void SetLevel0();

    public Transform GetSleepPlace(Transform human)
    {
        return m_SleepPlaces[Random.Range(0, m_SleepPlaces.Count)];
    }

    protected virtual Human IncreaseHuman()
    {
        Human asHuman = ResourceManager.S.LoadHuman("Prefabs/Humans/" + actData.HumanType.ToString());
        Human human = PoolManager.S.Spawn(asHuman, m_PosHuman);        
        human.Set(this, humanTemp.Clone(), m_Humans.Count);
        m_Humans.Add(human);
        return human;
    }

    private void IncreSpeedHuman(float value = 0.1f)
    {
        humanTemp.IncreaseSpeed(value);

        for (int i = 0; i < m_Humans.Count; i++)
        {
            m_Humans[i].IncreaseSpeed(value);
        }
    }

    protected virtual void IncreaseAnimal()
    {

    }

    protected virtual void IncreaseLand(int value)
    {

    }

    protected abstract void IncreaseQuantityAct(int value);

    protected virtual void SetType(ActType type)
    {
        actData.ActType = type;
        m_Signs.Set(actData.ActType);
    }

#if UNITY_EDITOR
    protected virtual void ToolSetType(ActType type)
    {
        actData.ActType = type;
        m_Signs.ToolSet(actData.ActType);
    }
#endif
}
