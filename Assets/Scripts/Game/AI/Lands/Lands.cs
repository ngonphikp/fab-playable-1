using BehaviorDesigner.Runtime;
using MEC;
using NPS;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SharedLands : SharedVariable<Lands>
{
    public static implicit operator SharedLands(Lands value) { return new SharedLands { Value = value }; }
}

[System.Serializable]
public class Lands : MonoBehaviour
{
    public CropEntity CropEntity => cropEntity;
    public Status GetStatus => m_Status;

    [Header("Object")]
    [SerializeField] private Spawner spawner;

    [Header("Properties")]
    private CropData cropTemp = new CropData();
    private CropEntity cropEntity;
    [SerializeField] private Status m_Status = Status.None;
    private GroundLand ground;

    public void Set(GroundLand ground)
    {
        this.ground = ground;

        cropEntity = DataManager.Data.Crop.Dictionary[ground.ActData.ActType];
        cropTemp.SetQuantity(cropEntity.Quantity);

        spawner.Set(this, cropTemp);        
    }

    public void SetStatus(Status status)
    {
        if (this.m_Status == status) return;

        m_Status = status;
        switch (status)
        {
            case Status.None:                
                break;
            case Status.Sowed:
                break;
            case Status.Watered:             
                break;
            case Status.Immatured:
                ground.ShowProgress(cropEntity.TimeMature, Mature);
                break;
            case Status.Matured:
                break;
            default:
                break;
        }
    }

    private Land GetLand(int index)
    {
        return (Land)spawner.Cells[index];
    }

    public bool CanSowing()
    {
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            if (GetLand(i).CanSowing()) return true;
        }
        return false;
    }

    public bool CanWatering()
    {
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            if (GetLand(i).CanWatering()) return true;
        }
        return false;
    }

    public bool CanGaining()
    {
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            if (GetLand(i).CanGaining()) return true;
        }
        return false;
    }

    public Land GetLandCanSowing(Transform human)
    {
        List<int> cans = new List<int>();
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            if (GetLand(i).CanSowing()) cans.Add(i);
        }        

        return GetLand(human, cans);
    }

    public Land GetLandCanWatering(Transform human)
    {
        List<int> cans = new List<int>();
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            if (GetLand(i).CanWatering()) cans.Add(i);
        }

        return GetLand(human, cans);
    }

    public Land GetLandCanGaining(Transform human)
    {
        List<int> cans = new List<int>();
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            if (GetLand(i).CanGaining()) cans.Add(i);
        }

        if (cans.Count > 0)
        {
            ground.HideProgress();
        }

        return GetLand(human, cans);
    }

    public Land GetLandRandom()
    {
        var land = GetLand(Random.Range(0, spawner.Cells.Count));
        return land;
    }

    private Land GetLand(Transform human, List<int> cans)
    {
        if (cans.Count == 0) return null;

        if (cans.Count == 1)
        {
            return GetLand(cans[0]);
        }

        int find = cans[0];
        float minDis = Vector3.Distance(human.position, GetLand(cans[0]).transform.position);
        for (int i = 1; i < cans.Count; i++)
        {
            float dis = Vector3.Distance(human.position, GetLand(cans[i]).transform.position);
            if (minDis > dis + 0.2f)
            {
                find = cans[i];
                minDis = dis;
            }
        }

        return GetLand(find);
    }

    public void HasSowed()
    {
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            if (GetLand(i).GetCrop.Data.Status < CropStatus.Seed) return;
        }
        SetStatus(Status.Sowed);
    }

    public void HasWatered()
    {
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            if (GetLand(i).GetCrop.Data.Status < CropStatus.Germ ) return;
        }
        SetStatus(Status.Watered);
    }

    public void HasImmatured()
    {
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            if (GetLand(i).GetCrop.Data.Status < CropStatus.Immature) return;
        }
        SetStatus(Status.Immatured);
    }

    public void HasMatured()
    {
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            if (GetLand(i).GetCrop.Data.Status < CropStatus.Mature) return;
        }
        SetStatus(Status.Matured);
    }

    public void HasGained()
    {
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            if (GetLand(i).GetCrop.Data.Status != CropStatus.None) return;
        }
        SetStatus(Status.None);
    }

    public void IncreaseColumnLeft()
    {
        spawner.IncreaseColumnLeft(this, cropTemp);
        SetStatus(Status.None);
    }

    public void IncreaseColumnRight()
    {
        spawner.IncreaseColumnRight(this, cropTemp);
        SetStatus(Status.None);
    }

    public void IncreaseRowTop()
    {
        spawner.IncreaseRowTop(this, cropTemp);
        SetStatus(Status.None);
    }

    public void IncreaseRowBottom()
    {
        spawner.IncreaseRowBottom(this, cropTemp);
        SetStatus(Status.None);
    }

    public void IncreaseQuatityCrop(int value)
    {
        cropTemp.IncreaseQuantity(value);
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            GetLand(i).GetCrop.IncreaseQuantity(value);
        }
    }

    public void UpgradeSuccess()
    {
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            GetLand(i).UpgradeSuccess();
        }
    }

    private void Mature()
    {
        for (int i = 0; i < spawner.Cells.Count; i++)
        {
            GetLand(i).SetStatus(Land.Status.Maturing);
        }
    }

    public enum Status
    {
        None = 0,
        Sowed,
        Watered,
        Immatured,
        Matured
    }
}
