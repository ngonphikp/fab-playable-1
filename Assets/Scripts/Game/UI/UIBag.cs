using NPS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using MEC;
using Random = UnityEngine.Random;

public class UIBag : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIActItem m_Coin;

    [SerializeField] private UIActItem m_ActPfb;
    [SerializeField] private Transform m_Content;

    private Dictionary<ActType, UIActItem> dicAct = new Dictionary<ActType, UIActItem>();
    private List<ActSort> acts = new List<ActSort>();
    private BagSave bag = new BagSave();

    private CoroutineHandle collectHandle;

    private void Start()
    {
        bag = DataManager.Save.Bag;

        foreach (KeyValuePair<ActType, int> item in bag.Act)
        {
            UIActItem ui = Instantiate(m_ActPfb, m_Content);
            ui.Set(item.Key);
            dicAct.Add(item.Key, ui);

            acts.Add(new ActSort()
            {
                Type = item.Key,
                Count = item.Value
            });
        }

        UpdateCoin();

        acts.OrderByDescending(x => x.Count);

        foreach (KeyValuePair<ActType, UIActItem> item in dicAct)
        {
            UpdateAct(item.Key);
        }

        bag.AddListener(BagChangeHandler);
    }

    private void OnDestroy()
    {
        bag.RemoveListener(BagChangeHandler);

        if (collectHandle.IsValid) Timing.KillCoroutines(collectHandle);
    }

    private void BagChangeHandler(object obj)
    {
        if (obj != null)
        {
            if (obj is ActType)
            {
                UpdateAct((ActType)obj);
            }
        }
        else UpdateCoin();
    }

    private void UpdateCoin()
    {
        m_Coin.Set(MathHelper.ScoreShow(bag.Currency[CurrencyType.Coin]));
    }

    private void UpdateAct(ActType type)
    {
        int count = bag.Act[type];
        if (count > 0)
        {
            dicAct[type].Set(MathHelper.ScoreShow(count));

            int remove = 0;
            for (int i = 0; i < acts.Count; i++)
            {
                if(acts[i].Type == type)
                {
                    remove = i;
                    break;
                }
            }

            acts.RemoveAt(remove);

            int find = 0;
            for (int i = 0; i < acts.Count; i++)
            {
                if(acts[i].Count < count)
                {
                    find = i;
                    break;
                }
            }

            acts.Insert(find, new ActSort()
            {
                Type = type,
                Count = count
            });

            dicAct[type].gameObject.transform.SetSiblingIndex(find);
        }
        dicAct[type].gameObject.SetActive(count > 0);
    }

    public void Collect(float mul)
    {
        if (collectHandle.IsValid) Timing.KillCoroutines(collectHandle);
        collectHandle = Timing.RunCoroutine(_Collect(mul));
    }

    public IEnumerator<float> _Collect(float mul)
    {
        Dictionary<ActType, int> change = new Dictionary<ActType, int>();
        var bag = DataManager.Save.Bag;

        foreach (KeyValuePair<ActType, int> item in bag.Act)
        {
            if (item.Value > 0)
            {
                int cost = DataManager.Data.Act.GetCost(item.Key);
                uint incre = (uint)(item.Value * cost * mul);

                change.Add(item.Key, item.Value);

                for (int i = 0; i < Random.Range(3, 5); i++)
                {
                    Dummy coin = PoolManager.S.Spawn(ResourceManager.S.DummyUI);
                    coin.Set(CurrencyType.Coin);
                    coin.Loot(GameManager.S.Player.transform, m_Coin.TranIcon, false);
                }                

                bag.IncreaseCurrency(CurrencyType.Coin, incre);

                yield return Timing.WaitForSeconds(0.05f);
            }
        }
        foreach (KeyValuePair<ActType, int> item in change)
        {
            bag.SubtractAct(item.Key, item.Value);
            yield return Timing.WaitForSeconds(0.05f);
        }
    }

    private IEnumerator<float> _Loot(CurrencyData currency)
    {

        for (int j = 0; j < Random.Range(3, 5); j++)
        {
            Dummy dummy = PoolManager.S.Spawn(ResourceManager.S.DummyUI);
            dummy.Set(CurrencyType.Coin);
            dummy.Loot(GameManager.S.Player.transform, m_Coin.TranIcon, false);
        }

        yield return Timing.WaitForSeconds(1.5f);
        bag.IncreaseCurrency(currency);
    }

    private class ActSort
    {
        public ActType Type;
        public int Count;
    }
}
