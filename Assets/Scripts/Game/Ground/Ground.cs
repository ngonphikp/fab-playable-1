using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;
using DG.Tweening;

public class Ground : Idenity
{
    public GroundData Data => data;
    public GroundSave Save => save;
    public GroupGround Group => group;

    [Header("Object")]
    [SerializeField] private Transform trees;

    [Header("Properties")]
    [SerializeField] private GroundData data;
    [SerializeField] private List<Transform> wcs = new List<Transform>();
    [SerializeField] private UnlockGround unlock;

    [Header("Event")]
    [SerializeField] private UnityEvent m_OnInitSuccess;
    [SerializeField] private UnityEvent m_OnLoadSuccess;

    private GroundSave save = new GroundSave();
    private GroupGround group;

#if UNITY_EDITOR
    private void OnValidate()
    {
        wcs.Clear();
        foreach (Transform tree in trees)
        {
            wcs.Add(tree);
        }

        unlock = this.GetComponent<UnlockGround>();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < data.Conditions.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, data.Conditions[i].gameObject.transform.position);
        }
    }
#endif

    protected virtual void Start()
    {
        
    }

    public virtual void Set(GroupGround group)
    {
        this.group = group;

        this.SetIndentity(group);
        this.Register();
      
        LoadData();        
        m_OnInitSuccess?.Invoke();

        LoadDataPrivate();
        Init();
    }

    protected virtual void LoadDataPrivate() 
    {

    }

    protected virtual void Init()
    {

    }

    public void LoadData()
    {
        save.SetLock(data.IsLock);
        save.SetCoin(data.Coin);

        save.Id = this.Id;
        save = DataManager.Save.Grounds(save);

        unlock.Init(save, data);
        unlock.m_OnUnlockSuccess.AddListener(UnlockHandler);

        unlock.LoadData();
        m_OnLoadSuccess?.Invoke();   
    }

    protected virtual void UnlockHandler()
    {
        
    }

    public void CheckOpen()
    {
        unlock.CheckOpen();
    }

    public virtual Store GetStore()
    {
        return null;
    }

    public virtual Store GetStore(Transform human)
    {
        return null;
    }

    public virtual Lands GetLands()
    {
        return null;
    }

    public virtual Transform GetWc(Transform human)
    {
        if (wcs.Count == 0)
        {
            return null;
        }

        if (wcs.Count == 1)
        {
            return wcs[0];
        }

        int find = 0;
        float minDis = Vector3.Distance(human.position, wcs[0].transform.position);
        for (int i = 1; i < wcs.Count; i++)
        {
            float dis = Vector3.Distance(human.position, wcs[i].transform.position);
            if (minDis > dis + 0.2f)
            {
                find = i;
                minDis = dis;
            }
        }

        return wcs[find];
    }

}
