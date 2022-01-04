using System;
using System.Collections.Generic;

[System.Serializable]

public class GroundUpgradeEntity
{
    public List<LevelGround> Levels = new List<LevelGround>();

    public void Insert(int index, LevelGround level)
    {
        Levels.Insert(index, level);
    }
}
