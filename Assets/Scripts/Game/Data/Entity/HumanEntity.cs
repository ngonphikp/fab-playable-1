using System;

[System.Serializable]
public class HumanEntity
{
    public HumanType Type = HumanType.Farm;
    public float Speed= 1f;
    public int MaxEnery = 10;
    public float RatioSleep = 0.03f;
    public float RatioWc = 0.03f;
    public int EnerySow = 1;
    public int EneryFeed = 1;
    public int EneryWater = 1;
    public int EneryGain = 1;
    public int EneryCollect = 1;

    public HumanEntity()
    {

    }

    public HumanEntity(DB_Human entity)
    {
        Enum.TryParse(entity.Get<string>("Type"), out HumanType humanType);
        Type = humanType;
        Speed = entity.Get<float>("Speed");
        MaxEnery = entity.Get<int>("MaxEnery");
        RatioSleep = entity.Get<float>("RatioSleep");
        RatioWc = entity.Get<float>("RatioWc");
        EnerySow = entity.Get<int>("EnerySow");
        EneryFeed = entity.Get<int>("EneryFeed");
        EneryWater = entity.Get<int>("EneryWater");
        EneryGain = entity.Get<int>("EneryGain");
        EneryCollect = entity.Get<int>("EneryCollect");
    }
}
