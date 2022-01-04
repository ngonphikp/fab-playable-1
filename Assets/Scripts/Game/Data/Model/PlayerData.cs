[System.Serializable]
public class PlayerData
{
    public PlayerStatus Status = PlayerStatus.Idle;
    public Direction Direction = Direction.Right;
    public float Speed = 4.5f;

    public PlayerData Clone()
    {
        PlayerData clone = new PlayerData();
        clone.Speed = Speed;
        return clone;
    }

    public void SetStatus(PlayerStatus value)
    {
        Status = value;
    }

    public void SetDirection(Direction direction)
    {
        Direction = direction;
    }

    public void IncreaseSpeed(float value)
    {
        Speed += value;
    }

    public void SetSpeed(float value)
    {
        Speed = value;
        if (Speed < 0) Speed = 0;
    }
}
