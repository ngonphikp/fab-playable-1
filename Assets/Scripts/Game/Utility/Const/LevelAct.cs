using BansheeGz.BGDatabase;
using System;

[System.Serializable]
public class LevelAct: LevelGround
{
    public UpActType Type;

    public LevelAct(BGEntity entity): base(entity)
    {
        Enum.TryParse(entity.Get<string>("TypeUp"), out UpActType typeUp);
        Type = typeUp;
    }
}
