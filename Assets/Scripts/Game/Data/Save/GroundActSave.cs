using System.Collections.Generic;

[System.Serializable]
public class GroundActSave: GroundUpgradeSave
{
    public List<HumanSave> Humans = new List<HumanSave>();

    public HumanSave GetHuman(int index)
    {
        if (Humans.Count <= index) Humans.Insert(index, new HumanSave());
        return Humans[index];
    }
}
