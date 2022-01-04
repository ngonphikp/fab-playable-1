using Sirenix.OdinInspector;
using System.Collections.Generic;

[System.Serializable]
public class StackVisualSave
{
    [ShowInInspector] public Dictionary<ActType, int> Count = new Dictionary<ActType, int>();

    public void ClearCount()
    {
        Count.Clear();
    }

    public void AddCount(ActType type)
    {
        if (!Count.ContainsKey(type)) Count.Add(type, 0);
        Count[type]++;
    }

    public void RemoveCount(ActType type)
    {
        Count[type]--;
    }
}
