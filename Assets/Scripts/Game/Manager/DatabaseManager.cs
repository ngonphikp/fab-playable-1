using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoSingleton<DatabaseManager>
{
    public GeneralTable General;
    public ActTable Act;
    public CropTable Crop;
    public HumanTable Human;
    public GroundLandTable GroundLand;
    public GroundStoreTable GroundStore;
    public PlayerTable Player;

    private void Start()
    {

    }

    public void Init(Transform parent = null)
    {
        DataManager.Data = this;
        if (parent) transform.SetParent(parent);

        General = new GeneralTable();
        Act = new ActTable();
        Crop = new CropTable();
        Human = new HumanTable();
        GroundLand = new GroundLandTable();
        GroundStore = new GroundStoreTable();
        Player = new PlayerTable();
    }
}
