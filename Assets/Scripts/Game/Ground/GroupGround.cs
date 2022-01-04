using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupGround : Idenity
{
    public List<Ground> Grounds => m_Grounds;

    [SerializeField] private List<Ground> m_Grounds = new List<Ground>();
    private List<GroundStore> stores = new List<GroundStore>();

#if UNITY_EDITOR
    private void OnValidate()
    {
        m_Grounds.Clear();
        foreach (Transform transform in this.transform)
        {
            m_Grounds.Add(transform.GetComponent<Ground>());
        }
    }
#endif

    public void Set()
    {
        SetIndentity();
        Register();

        for (int i = 0; i < m_Grounds.Count; i++)
        {
            m_Grounds[i].Set(this);
            if (m_Grounds[i] as GroundStore)
            {
                stores.Add(m_Grounds[i] as GroundStore);
            }
        }
    }

    private void Start()
    {
        
    }

    public Store GetStore(Transform human, ActType type)
    {
        Store find = null;
        float minDis = 0f;

        for (int i = 0; i < stores.Count; i++)
        {
            if (stores[i].Save.IsLock) continue;

            var store = stores[i].GetStore();
            if (stores[i].StoreData.Type == type && stores[i].StoreSave.Count < stores[i].StoreData.MaxCount)
            {
                float dis = Vector3.Distance(human.position, store.transform.position);
                if (!find)
                {
                    find = store;
                    minDis = dis;
                }

                if (minDis > dis + 0.2f)
                {
                    find = store;
                    minDis = dis;
                }
            }
        }
        return find;
    }

    public List<GroundStore> GetStores(ActType type)
    {
        List<GroundStore> finds = new List<GroundStore>();

        for (int i = 0; i < stores.Count; i++)
        {
            if (stores[i].Save.IsLock) continue;

            if (stores[i].StoreData.Type == type && stores[i].StoreSave.Count < stores[i].StoreData.MaxCount)
            {
                finds.Add(stores[i]);
            }
        }
        return finds;
    }

    public Transform GetWc(Transform human)
    {
        Transform find = null;
        float minDis = 0f;

        for (int i = 0; i < m_Grounds.Count; i++)
        {
            Transform min = m_Grounds[i].GetWc(human);
            if (min)
            {
                float dis = Vector3.Distance(human.position, min.transform.position);
                if (!find)
                {
                    find = min;
                    minDis = dis;
                }

                if (minDis > dis + 0.2f)
                {
                    find = min;
                    minDis = dis;
                }
            } 
        }
        return find;
    }

    public void CheckOpen()
    {
        for (int i = 0; i < m_Grounds.Count; i++)
        {
            if (m_Grounds[i].Save.IsLock) m_Grounds[i].CheckOpen();
        }
    }

    public void CheckRequire()
    {
        for (int i = 0; i < m_Grounds.Count; i++)
        {
            if (m_Grounds[i].Save.IsLock) m_Grounds[i].CheckOpen();
        }
    }

    public void Collect(bool isCreBag = false)
    {
        for (int i = 0; i < m_Grounds.Count; i++)
        {
            if (m_Grounds[i] is GroundStore)
            {
                (m_Grounds[i] as GroundStore).Collect(isCreBag);
            }
        }
    }
}
