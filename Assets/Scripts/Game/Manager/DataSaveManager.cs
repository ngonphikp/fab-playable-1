using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataSaveManager : MonoSingleton<DataSaveManager>
{
    #region Bag
    public BagSave Bag
    {
        get => bag;
        set { bag = value; }
    }

    [SerializeField] private BagSave bag = new BagSave();
    #endregion
    #region Grounds
    public GroundSave Grounds(GroundSave data)
    {
        if (grounds.ContainsKey(data.Id)) return grounds[data.Id];
        else
        {
            grounds.Add(data.Id, data);
            return data;
        }
    }

    [ShowInInspector] private Dictionary<string, GroundSave> grounds = new Dictionary<string, GroundSave>();
    #endregion
    #region Stores
    public Dictionary<string, GroundStoreSave> DicStores => stores;
    public GroundStoreSave Stores(GroundStoreSave data)
    {
        if (stores.ContainsKey(data.Id)) return stores[data.Id];
        else
        {
            stores.Add(data.Id, data);
            return data;
        }
    }

    [ShowInInspector] private Dictionary<string, GroundStoreSave> stores = new Dictionary<string, GroundStoreSave>();
    #endregion
    #region Acts
    public Dictionary<string, GroundActSave> DicActs => acts;
    public GroundActSave Acts(GroundActSave data)
    {
        if (acts.ContainsKey(data.Id)) return acts[data.Id];
        else
        {
            acts.Add(data.Id, data);
            return data;
        }
    }

    [ShowInInspector] private Dictionary<string, GroundActSave> acts = new Dictionary<string, GroundActSave>();
    #endregion
    #region StackVisual
    public StackVisualSave StackVisual
    {
        get => stackVisual;
        set { stackVisual = value; }
    }

    [SerializeField] private StackVisualSave stackVisual = new StackVisualSave();
    #endregion
    #region Player
    public PlayerSave Player
    {
        get => player;
        set { player = value; }
    }

    [SerializeField] private PlayerSave player = new PlayerSave();
    #endregion
    #region General
    public GeneralSave General
    {
        get => general;
        set { general = value; }
    }

    [SerializeField] private GeneralSave general = new GeneralSave();
    #endregion

    public void Init(Transform parent = null)
    {
        DataManager.Save = this;
        if (parent) transform.SetParent(parent);
    }
}
