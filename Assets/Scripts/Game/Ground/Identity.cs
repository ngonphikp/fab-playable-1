using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idenity: MonoBehaviour
{
    public string Id = string.Empty;

    public virtual void SetIndentity(Idenity parent = null)
    {
        string id = "";

        if(parent) id += parent.Id + "-";

        id += this.gameObject.name;

        this.Id = id;
    }

    public void Register()
    {
        DataManager.Indenity?.Register(this);
    }
}
