using System.Collections.Generic;

[System.Serializable]
public class GroundStoreData: GroundUpgradeData
{
    public ActType Type = ActType.Corn;
    public int MaxCount = 1000;

    public void IncreaseMaxCount(int value)
    {
        MaxCount += value;
    }
}
