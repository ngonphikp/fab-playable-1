using BansheeGz.BGDatabase;
using System;

[System.Serializable]
public class LevelFactory: LevelGround
{
    public UpFactoryType Type;

    public LevelFactory(BGEntity entity): base(entity)
    {
        Enum.TryParse(entity.Get<string>("TypeUp"), out UpFactoryType typeUp);
        Type = typeUp;
    }
}
