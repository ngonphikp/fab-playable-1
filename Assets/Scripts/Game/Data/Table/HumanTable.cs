using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanTable
{
    [ShowInInspector] public Dictionary<HumanType, HumanEntity> Dictionary = new Dictionary<HumanType, HumanEntity>();

    public HumanTable()
    {
        Dictionary.Clear();
        DB_Human.ForEachEntity(entity =>
        {
            if (entity != null)
            {
                HumanEntity human = new HumanEntity(entity);
                Dictionary.Add(human.Type, human);
            }
        });
    }
}
