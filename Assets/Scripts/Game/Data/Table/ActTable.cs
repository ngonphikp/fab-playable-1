using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActTable
{
    [ShowInInspector] private Dictionary<ActType, int> Dictionary = new Dictionary<ActType, int>();

    public ActTable()
    {
        Dictionary.Clear();
        DB_Act.ForEachEntity(entity =>
        {
            if (entity != null)
            {
                Enum.TryParse(entity.Get<string>("Type"), out ActType actType);
                var Type = actType;
                int Cost = entity.Get<int>("Cost");
                Dictionary.Add(Type, Cost);
            }
        });
    }

    public int GetCost(ActType type)
    {
        return Mathf.RoundToInt(Dictionary[type]);
    }
}
