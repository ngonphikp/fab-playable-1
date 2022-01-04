using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroundData
{
    public bool IsLock = true;
    public int Coin = -2;
    public int Exp = 0;
    public int Level = 0;
    [HideInInspector] public int SumExp = 0;

    public List<Ground> Conditions = new List<Ground>();
}
