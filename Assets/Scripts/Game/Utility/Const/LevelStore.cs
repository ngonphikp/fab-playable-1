using BansheeGz.BGDatabase;
using System;

[System.Serializable]
public class LevelStore: LevelGround
{
    public UpStoreType Type;

    public LevelStore(BGEntity entity): base(entity)
    {
        Enum.TryParse(entity.Get<string>("TypeUp"), out UpStoreType typeUp);
        Type = typeUp;
    }
}
