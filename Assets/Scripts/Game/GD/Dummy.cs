using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dummy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer visual;
    [SerializeField] private GameObject ef;
    private ActType type;

    public void Set(ActType type)
    {
        this.type = type;

        visual.sprite = ResourceManager.S.LoadSprite("Icons", type.ToString());
    }

    public void Set(string content)
    {
        visual.sprite = ResourceManager.S.LoadSprite("Icons", content);
    }

    public void Set(CurrencyType type)
    {
        visual.sprite = ResourceManager.S.LoadSprite("Icons", type.ToString());
    }

    public void Set(bool isEf)
    {
        if (ef) ef.gameObject.SetActive(isEf);
    }

    public void Loot(Transform start, Transform end, Action action)
    {
        this.transform.position = start.position;
        this.transform.DOLocalJump(end.position, 2f, 1, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => 
        { 
            PoolManager.S.Despawn(this);
            action?.Invoke();
        });
    }

    public void Loot(Transform start, Transform end, bool isEf = true, Action action = null)
    {
        Set(false);
        this.transform.position = start.position;
        this.transform.DOJump(start.position + new Vector3(Random.Range(0.15f, 0.6f) * (Random.Range(0, 99) % 2 == 0 ? 1 : -1), Random.Range(0.15f, 0.6f) * (Random.Range(0, 99) % 2 == 0 ? 1 : -1)), 3, 1, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            this.transform.DOMove(transform.position, Random.Range(0.25f, 0.75f)).OnComplete(() =>
            {
                this.transform.SetParent(end);
                Set(isEf);

                this.transform.DOLocalJump(Vector3.zero, 0.5f, 1, 1.0f).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                    PoolManager.S.Despawn(this);                    
                    if (action != null)
                    {
                        action();
                    }    
                });
            });
        });
    }
}
