using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerTable
{
    public List<PlayerEntity> List = new List<PlayerEntity>();

    public PlayerTable()
    {
        List.Clear();
        DB_Player.ForEachEntity(entity =>
        {
            if (entity != null)
            {
                int index = entity.Get<int>("Index");
                PlayerEntity player = new PlayerEntity(entity);
                List.Insert(index, player);
            }
        });
    }
}
