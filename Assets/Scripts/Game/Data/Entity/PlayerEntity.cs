using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerEntity
{
    public int MaxExp;

    public PlayerEntity()
    {

    }

    public PlayerEntity(DB_Player entity)
    {
        MaxExp = entity.Get<int>("MaxExp");
    }
}
