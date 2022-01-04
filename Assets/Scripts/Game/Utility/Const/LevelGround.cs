using BansheeGz.BGDatabase;

[System.Serializable]
public class LevelGround
{
    public int Cost;
    public float Value;
    public string Visual;
    public int Exp;
    public float Idle;

    public LevelGround(BGEntity entity)
    {
        Cost = entity.Get<int>("Cost");
        Value = entity.Get<float>("Value");
        Visual = entity.Get<string>("Visual");
        Exp = entity.Get<int>("Exp");
        Idle = entity.Get<float>("Idle");
    }
}
