using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SharedStore : SharedVariable<Store>
{
    public static implicit operator SharedStore(Store value) { return new SharedStore { Value = value }; }
}

public class Store : MonoBehaviour
{
    public ActType Type => ground.StoreData.Type;

    [Header("Object")]
    [SerializeField] private Transform m_Door;

    [Header("Properties")]
    private GroundStore ground;

    public void Set(GroundStore ground)
    {
        this.ground = ground;
    }    

    public Transform GetDoor()
    {
        return m_Door;
    }

    public Transform GetProgress()
    {
        return ground.GetProgress();
    }

    public void IncreaseAct(int value)
    {
        ground.IncreaseAct(value);
    }

    public void SubstractAct(int value)
    {
        ground.SubstractAct(value);
    }
}
