using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MEC;
using System;
using System.Linq;
using Random = UnityEngine.Random;

public class StackVisualAct : MonoBehaviour
{
    [SerializeField] private AutoGrid grid;
    [SerializeField] private GameObject box;
    [SerializeField] private VisualAct visual;

    private List<VisualAct> acts = new List<VisualAct>();
    private StackVisualSave stackVisual;

    private void Start()
    {
        stackVisual = DataManager.Save.StackVisual;

        foreach (KeyValuePair<ActType, int> item in stackVisual.Count)
        {
            for (int i = 0; i < item.Value; i++)
            {
                VisualAct act = PoolManager.S.Spawn(visual, grid.transform);
                act.Set(item.Key);
                grid.AddElement(act.gameObject);

                acts.Add(act);
            }
        }
    }

    public void IncreaseAct(int amount, ActType actType, Vector3 startPos)
    {
        Timing.RunCoroutine(_IncreaseAct(amount, actType, startPos));
    }

    private IEnumerator<float> _IncreaseAct(int amount, ActType actType, Vector3 startPos)
    {
        box.SetActive(true);

        int number = amount;
        number = Mathf.Clamp(number, 1, 15);
        for (int i = 0; i < number; i++)
        {
            VisualAct act = PoolManager.S.Spawn(visual, grid.transform);
            act.Set(actType);

            Dummy dm = PoolManager.S.Spawn(ResourceManager.S.Dummy);
            dm.Set(actType);
            dm.transform.position = startPos;

            act.Set(false);
           
            dm.transform.SetParent(act.transform);
            dm.transform.DOLocalMove(Vector2.zero, 0.3f).SetEase(Ease.OutQuad).OnComplete(() => { act.Set(true); PoolManager.S.Despawn(dm);
                countIncre++;
                if (countIncre % 3 == 0)
                {
                    countIncre = 0;
                }
            });
         
           
            AddAct(act);
            grid.AddElement(act.gameObject);
            yield return Timing.WaitForSeconds(0.02f); 
        }
        countIncre = 2;
    }

    int countIncre = 2;
    int countSubtract = 2;

    public IEnumerator<float> _SubstractAct(ActType type, Vector3 endPos, float ratio = 1.0f, Action complete = null)
    {
        box.SetActive(true);
        int number = ratio == 1.0f ? stackVisual.Count[type] : Mathf.RoundToInt(stackVisual.Count[type] * ratio);

        int loot = 0;
        foreach (var act in acts.ToList())
        {
            if (number <= 0) break;
            if (act.Type == type)
            {
                loot++;
                number--;
                Dummy dm = PoolManager.S.Spawn(ResourceManager.S.Dummy);
                dm.Set(type);
                dm.transform.position = act.transform.position;

                dm.transform.DOMove(endPos, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    PoolManager.S.Despawn(act);
                    grid.SubjectElement();
                    countSubtract++;
                    if (countSubtract % 3 == 0)
                    {
                        countSubtract = 0;
                    }
                    RemoveAct(act);

                    loot--;                    
                    PoolManager.S.Despawn(dm);
                });

                yield return Timing.WaitForSeconds(0.02f);
            }
        }
        countSubtract = 2;
        while (true)
        {
            if (loot == 0) break;
            //Debug.Log("Waiting Loot");
            yield return Timing.WaitForOneFrame;
        }

        box.SetActive(acts.Count <= 0);
        yield return Timing.WaitForSeconds(0.1f);
        grid.UpdateGrid();
        complete?.Invoke();
    }

    public void Collect()
    {
        for (int i = 0; i < acts.Count; i++)
        {
            PoolManager.S.Despawn(acts[i]);
        }

        ClearAct();
        grid.count = 0;        
    }

    public void ClearAct()
    {
        acts.Clear();
        stackVisual.ClearCount();
    }

    public void AddAct(VisualAct act)
    {
        acts.Add(act);
        stackVisual.AddCount(act.Type);
    }

    public void RemoveAct(VisualAct act)
    {
        acts.Remove(act);
        stackVisual.RemoveCount(act.Type);
    }
}
