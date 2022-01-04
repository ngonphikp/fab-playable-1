using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroundStoreTable
{
    public GroundStoreEntity Data = new GroundStoreEntity();

    public GroundStoreTable()
    {
        DB_GroundStore.ForEachEntity(entity =>
        {
            if (entity != null)
            {   
                int index = entity.Get<int>("Index");
                Data.Levels.Insert(index, new LevelStore(entity));
            }
        });
    }
}
