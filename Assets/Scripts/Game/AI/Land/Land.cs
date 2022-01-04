using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SharedLand : SharedVariable<Land>
{
    public static implicit operator SharedLand(Land value) { return new SharedLand { Value = value }; }
}

[System.Serializable]
public class Land : Cell
{
    public Status GetStatus => m_Status;
    public Crop GetCrop => m_Crop;
    public CropData Data => data;
    public CropEntity Entity => lands.CropEntity;

    [Header("Object")]
    [SerializeField] private GameObject m_EfUpgrade;

    [Header("Properties")]
    [SerializeField] private Sprite m_SpWet;
    [SerializeField] private Sprite m_SpDrought;
    [SerializeField] private SpriteRenderer m_Graphic;
    
    private Crop m_Crop;
    private CropData data;
    private Status m_Status = Status.Drought;
    private Lands lands;
    private Human human;

    public void Set(Lands lands, CropData data)
    {
        this.lands = lands;
        this.data = data;

        SetStatus(Status.Drought);        

        Crop asCrop = ResourceManager.S.LoadCrop("Prefabs/Crops/" + Entity.Type.ToString());

        m_Crop = PoolManager.S.Spawn(asCrop, this.transform);
        m_Crop.Set(this);
        
        switch (lands.GetStatus)
        {
            case Lands.Status.None:
                break;
            case Lands.Status.Sowed:
                break;
            case Lands.Status.Watered:
                break;
            case Lands.Status.Immatured:
                break;
            case Lands.Status.Matured:
                m_Graphic.sprite = m_SpWet;
                m_Crop.SetStatus(CropStatus.Maturing);
                break;
            default:
                break;
        }
    }

    public void SetStatus(Status status)
    {
        if (this.m_Status == status) return;

        m_Status = status;
        switch (status)
        {
            case Status.Drought:
                m_Graphic.sprite = m_SpDrought;
                break;
            case Status.Wet:
                m_Graphic.sprite = m_SpWet;
                break;
            case Status.Watering:
                Water();
                break;
            case Status.Sowing:
                Snow();
                break;
            case Status.Maturing:
                Mature();
                break;
            case Status.Gaining:
                Gain();
                break;
            default:
                break;
        }
    }    

    private void Snow()
    {
        m_Crop.SetStatus(CropStatus.Sowing);
    }

    private void Water()
    {
        SetStatus(Status.Wet);
        m_Crop.SetStatus(CropStatus.Growing);
        HasWatered();
    }

    private void Mature()
    {
        if (m_Crop.Data.Status == CropStatus.Immature)
        {
            m_Crop.SetStatus(CropStatus.Maturing);
        }
    }

    private void Gain()
    {
        m_Crop.SetStatus(CropStatus.Gaining);
    }    

    public bool CanSowing()
    {
        return m_Crop.Data.Status == CropStatus.None && human == null;
    }

    public bool CanWatering()
    {
        return m_Crop.Data.Status == CropStatus.Germ && human == null;
    }

    public bool CanMaturing()
    {
        return m_Crop.Data.Status == CropStatus.Immature && human == null;
    }

    public bool CanGaining()
    {
        return m_Crop.Data.Status == CropStatus.Mature && human == null;
    }

    public void Register(Human human)
    {
        if (this.human)
        {
            if (this.human == human)
            {
                Debug.LogError("You are already registered in this land");
            }
            else
            {
                Debug.LogError("Other human is already registered in this land");
            }
            return;
        }
        this.human = human;
    }

    public void Deregister(Human human = null)
    {
        if (human == null || this.human == human)
        {
            this.human = null;
        }
        else
        {
            Debug.LogError("Human don't work in this land");
        }
    }

    public void HasSowed()
    {
        lands.HasSowed();
    }

    public void HasWatered()
    {
        lands.HasWatered();
    }

    public void HasImmatured()
    {
        lands.HasImmatured();
    }

    public void HasMatured()
    {
        lands.HasMatured();
    }

    public void HasGained()
    {
        SetStatus(Status.Drought);
        lands.HasGained();
    }

    public void UpgradeSuccess()
    {
        PoolManager.S.Spawn(m_EfUpgrade, this.transform);
    }

    public enum Status
    {
        Drought = 0,
        Wet,
        Sowing,
        Watering,
        Maturing,
        Gaining
    }
}
