using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CropTable
{
    [ShowInInspector] public Dictionary<ActType, CropEntity> Dictionary = new Dictionary<ActType, CropEntity>();

    public CropTable()
    {
        Dictionary.Clear();
        DB_Crop.ForEachEntity(entity =>
        {
            if (entity != null)
            {
                CropEntity crop = new CropEntity(entity);
                Dictionary.Add(crop.Type, crop);
            }
        });
    }
}
