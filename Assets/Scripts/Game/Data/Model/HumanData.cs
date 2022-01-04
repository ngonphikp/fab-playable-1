[System.Serializable]
public class HumanData
{
    public HumanStatus Status = HumanStatus.Idle;
    public float Speed = 1.0f;
    public bool Sleep = false;
    public bool Wc = false;
    public Direction Direction = Direction.Right;
    public int Eat = -1;

    public HumanData Clone()
    {
        HumanData clone = new HumanData();
        clone.Speed = Speed;
        return clone;
    }

    public void SetStatus(HumanStatus value)
    {
        Status = value;
    }    

    public void SetSleep(bool value)
    {
        Sleep = value;
    }

    public void SetWc(bool value)
    {
        Wc = value;
    }       

    public void SetSpeed(float value)
    {
        Speed = value;
        if (value < 0) Speed = 0;
    }

    public void IncreaseSpeed(float value)
    {
        Speed += value;
    }

    public void SetDirection(Direction direction)
    {
        Direction = direction;
    }

    public void SetEat(int value)
    {
        Eat = value;
    }
}
