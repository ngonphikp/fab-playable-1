[System.Serializable]

public class GroundUpgradeSave
{
    public string Id = string.Empty;

    public int Level = 0;
    public int CoinUp = -2;

    public void SetCoinUp(int value)
    {
        CoinUp = value;
    }

    public void SubtractCoinUp(int value)
    {
        CoinUp -= value;
        if (CoinUp < 0) CoinUp = 0;
    }

    public void SetLevel(int value)
    {
        Level = value;
    }

    public void IncreaseLevel(int value)
    {
        Level += value;
    }
}
