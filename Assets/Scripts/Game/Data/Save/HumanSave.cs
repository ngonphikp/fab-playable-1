using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanSave
{
    public int Enery = -1;
    public int Act = 0;

    public void IncreaseEnery(int value, int max = 0)
    {
        Enery += value;

        if (max != 0 && Enery > max) Enery = max;
    }

    public void SubstractEnery(int value)
    {
        //if (AbilityManager.S.IsNoHungryHuman) return;

        Enery -= value;

        if (Enery < 0) Enery = 0;
    }

    public void SetEnery(int value, int max = 0)
    {
        if (value < 0) return; //&& AbilityManager.S.IsNoHungryHuman

        Enery = value;

        if (Enery < 0) Enery = 0;
        if (max != 0 && Enery > max) Enery = max;
    }

    public void IncreaseAct(int value)
    {
        Act += value;
    }

    public void SubstractAct(int value)
    {
        Act -= value;
        if (Act < 0) Act = 0;
    }

    public void SetAct(int value)
    {
        Act = value;
        if (Act < 0) Act = 0;
    }
}
