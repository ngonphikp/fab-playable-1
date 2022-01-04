using BansheeGz.BGDatabase;
using System;

[System.Serializable]
public class LevelCanteen: LevelGround
{
    public UpCanteenType Type;

    public LevelCanteen(BGEntity entity) : base(entity)
    {
        Enum.TryParse(entity.Get<string>("TypeUp"), out UpCanteenType typeUp);
        Type = typeUp;
    }
}
