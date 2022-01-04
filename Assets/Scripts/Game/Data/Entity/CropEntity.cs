using System;

[System.Serializable]
public class CropEntity
{
    public ActType Type = ActType.Corn;
    public float TimeGerm = 0.01f;
    public float TimeGrow = 0.01f;
    public float TimeMature = 5f;
    public int Quantity = 10;

    public CropEntity()
    {

    }

    public CropEntity(DB_Crop entity)
    {
        Enum.TryParse(entity.Get<string>("Type"), out ActType actType);
        Type = actType;
        TimeGerm = entity.Get<float>("TimeGerm");
        TimeGrow = entity.Get<float>("TimeGrow");
        TimeMature = entity.Get<float>("TimeMature");
        Quantity = entity.Get<int>("Quantity");
    }
}
