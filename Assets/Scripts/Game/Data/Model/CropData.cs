[System.Serializable]
public class CropData
{
    public CropVisual Visual = CropVisual.None;
    public CropStatus Status = CropStatus.None;
    public int Quantity = 10;

    public CropData Clone()
    {
        CropData clone = new CropData();

        clone.Visual = Visual;
        clone.Quantity = Quantity;

        return clone;
    }

    public void IncreaseQuantity(int value)
    {
        Quantity += value;
    }

    public void SetQuantity(int value)
    {
        Quantity = value;
    }

    public void SetVisual(CropVisual value)
    {
        Visual = value;
    }

    public void SetStatus(CropStatus value)
    {
        Status = value;
    }
}
