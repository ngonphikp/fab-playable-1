using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSave
{
    public Vector3 Position = new Vector3(0, 0, -1);
    public int Level = -1;
    public int Exp = 0;

    public List<int> Pet = new List<int>();
    public int Skin = 0;

    public void SetPosition(Vector3 value)
    {
        Position = value;
    }

    public void SetLevel(int value)
    {
        Level = value;
        if (Level < 0) Level = 0;
    }

    public void SetExp(int value)
    {
        Exp = value;
        if (Exp < 0) Exp = 0;
    }

    public void IncreaseExp(int value)
    {
        Exp += value;
    }

    public void IncreaseLevel(int value)
    {
        Level += value;
    }

    public void SetSkin(int value)
    {
        Skin = value;
    }

    public void IncreasePet(int value)
    {
        if (Pet.Contains(value)) return;
        Pet.Add(value);
    }
}
