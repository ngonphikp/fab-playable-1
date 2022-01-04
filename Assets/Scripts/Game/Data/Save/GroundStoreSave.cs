[System.Serializable]
public class GroundStoreSave: GroundUpgradeSave
{
    public ActType Type = ActType.Corn;
    public int Count = 0;

    public void IncreaseCount(int value, int max = 0)
    {
        Count += value;
        if (max != 0 && Count > max) Count = max;
    }

    public void SubtractCount(int value)
    {
        Count -= value;
        if (Count < 0) Count = 0;
    }

    public void SetCount(int value, int max = 0)
    {
        Count += value;
        if (max != 0 && Count > max) Count = max;
        if (Count < 0) Count = 0;
    }

    public void SetType(ActType value)
    {
        Type = value;
    }
}
