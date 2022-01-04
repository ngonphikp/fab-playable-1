using MEC;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S;
    public Player Player => m_Player;

    [SerializeField] private GameObject m_Content;
    [SerializeField] private List<GroupGround> m_GroupGrounds = new List<GroupGround>();
    [SerializeField] private Player m_Player;

    private GeneralSave general;

    public bool isReward = false;

    private void Awake()
    {
        if (!S) S = this;

        DataManager.S.Init();
    }

    private void Start()
    {
        foreach (var item in m_GroupGrounds)
        {
            item.Set();
        }

        OpenSucess();

        CalculatorExp();

        m_Player.Set();
    }

    private void CalculatorExp()
    {
        if (DataManager.Save.Player.Level >= 0) return;        

        int exp = 0;
        int level = 0;

        foreach (var group in m_GroupGrounds)
        {
            foreach (var ground in group.Grounds)
            {
                if (!ground.Save.IsLock)
                {
                    exp += ground.Data.SumExp;
                }
            }
        }
        
        foreach (var entity in DataManager.Data.Player.List)
        {
            if (exp >= entity.MaxExp)
            {
                exp -= entity.MaxExp;
                level++;
            }
        }

        if (level > DataManager.Data.Player.List.Count - 1 || (level == DataManager.Data.Player.List.Count - 1 && exp > DataManager.Data.Player.List[level].MaxExp))
        {
            level = DataManager.Data.Player.List.Count - 1;
            exp = DataManager.Data.Player.List[level].MaxExp;
        }

        DataManager.Save.Player.SetLevel(level);
        DataManager.Save.Player.SetExp(exp);

        GameScene.S.Profile.UpdateLevel();
        UpLevelSucess();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (m_Content)
        {
            m_GroupGrounds.Clear();
            foreach (Transform transform in m_Content.transform)
            {
                m_GroupGrounds.Add(transform.GetComponent<GroupGround>());
            }
        }
    }
#endif

    public void OpenSucess()
    {
        for (int i = 0; i < m_GroupGrounds.Count; i++)
        {
            m_GroupGrounds[i].CheckOpen();
        }
    }

    public void UpLevelSucess()
    {
        for (int i = 0; i < m_GroupGrounds.Count; i++)
        {
            m_GroupGrounds[i].CheckRequire();
        }
    }
}
