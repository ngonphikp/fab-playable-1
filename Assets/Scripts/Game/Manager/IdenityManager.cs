using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdenityManager : MonoSingleton<IdenityManager>
{
    [ShowInInspector] Dictionary<string, Idenity> Root = new Dictionary<string, Idenity>();

    private void Start()
    {
        
    }

    public void Init(Transform parent = null)
    {
        DataManager.Indenity = this;
        if (parent) transform.SetParent(parent);
    }

    public void Register(Idenity obj)
    {
        if (Root.ContainsKey(obj.Id))
        {
            Debug.LogError("Loop Id: " + obj.Id);
            return;
        }
        else
        {
            Root.Add(obj.Id, obj);
        }
    }
}
