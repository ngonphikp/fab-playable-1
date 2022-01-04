[System.Serializable]
public class GroundSave
{
    public string Id = string.Empty;
    public bool IsLock = true;
    public int Coin = -2;

    public void SetLock(bool value)
    {
        IsLock = value;
    }

    public void SetCoin(int value)
    {
        Coin = value;
    }

    public void SubtractCoin(int value)
    {
        Coin -= value;
        if (Coin < 0) Coin = 0;
    }
}
