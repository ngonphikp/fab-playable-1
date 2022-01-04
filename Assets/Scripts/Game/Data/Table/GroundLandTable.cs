using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroundLandTable
{
    [ShowInInspector] public Dictionary<ActType, GroundLandEntity> Dictionary = new Dictionary<ActType, GroundLandEntity>();

    public GroundLandTable()
    {
        Dictionary.Clear();
        DB_GroundLand.ForEachEntity(entity =>
        {
            if (entity != null)
            {
                Enum.TryParse(entity.Get<string>("Type"), out ActType actType);
                int index = entity.Get<int>("Index");

                if (!Dictionary.ContainsKey(actType)) Dictionary.Add(actType, new GroundLandEntity() { Type = actType });
                Dictionary[actType].Levels.Insert(index, new LevelAct(entity));
            }
        });
    }
}
