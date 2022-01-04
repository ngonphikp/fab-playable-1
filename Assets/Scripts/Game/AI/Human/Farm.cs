using BehaviorDesigner.Runtime;
using DG.Tweening;
using MEC;
using Spine.Unity;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[System.Serializable]
public class Farm : Human
{
    [Header("Object")]
    [SerializeField] private Transform m_Fire;
    [SerializeField] private GameObject m_SeedRight;
    [SerializeField] private GameObject m_SeedLeft;

    public void Sow(Land land)
    {
        var seed = PoolManager.S.Spawn(m_Content.transform.localScale.x == 1 ? m_SeedRight : m_SeedLeft);
        seed.transform.position = m_Fire.position;

        land.SetStatus(Land.Status.Sowing);
    }

    public void Water(Land land)
    {
        land.SetStatus(Land.Status.Watering);
    }

    public void GainCrop(Land land)
    {
        for (int i = 0; i < 4; i++)
        {
            Dummy dm = PoolManager.S.Spawn(ResourceManager.S.Dummy);
            dm.Set(land.Entity.Type);
            dm.transform.position = land.transform.position;

            dm.transform.DOLocalJump(land.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 1), 0.25f, 1, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                dm.gameObject.transform.SetParent(this.gameObject.transform);
                dm.transform.DOLocalJump(Vector3.zero, 2f, 1, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => { PoolManager.S.Despawn(dm); });
            });
        }        

        land.SetStatus(Land.Status.Gaining);
        save.IncreaseAct(land.Entity.Quantity);
    }
}
