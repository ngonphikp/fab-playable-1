using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;
using DG.Tweening;

public class UpgradeGround : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private InteractCircle m_Interact;
    [SerializeField] private GameObject m_EfUpgrade;

    [Header("UI")]
    [SerializeField] private UIUpgrade m_Upgrade;
    [SerializeField] protected UIToastUpgrade m_ToastUp;

    [Header("Properties")]
    private GroundData Data;
    private GroundUpgradeData data;
    private GroundUpgradeSave save;
    private GroundUpgradeEntity entity;

    [Header("Event")]
    public UnityEvent<int> m_OnUpgradeHandler;
    public UnityEvent<int> m_OnUpgradeSuccess;
    public UnityEvent<int> m_OnShowAds;

    private CoroutineHandle handle;

    private void Start()
    {
        m_Upgrade.gameObject.SetActive(false);
    }

    public void Init(GroundData Data, GroundUpgradeSave save, GroundUpgradeData data, GroundUpgradeEntity entity)
    {
        this.Data = Data;
        this.save = save;
        this.data = data;
        this.entity = entity;

        if (save.CoinUp == -2)
        {
            if (save.Level == 0)
            {
                int cost = entity.Levels[0].Cost;
                save.SetCoinUp((int)(cost * (cost > 0 ? data.Ratio : 1)));
                m_Upgrade.SetLevel(save.Level);
                m_Upgrade.SetCoin(save.CoinUp);
            }
            else if (save.Level < entity.Levels.Count)
            {
                int cost = entity.Levels[entity.Levels.Count - 1].Cost;
                save.SetCoinUp((int)(cost * (cost > 0 ? data.Ratio : 1)));
                m_Upgrade.SetLevel(save.Level);
                m_Upgrade.SetCoin(save.CoinUp);
            }
            else
            {
                m_Upgrade.SetMaxLevel();
            }
        }
        else
        {
            m_Upgrade.SetLevel(save.Level);
            m_Upgrade.SetCoin(save.CoinUp);
        }
    }

    public void Upgrade(int level)
    {
        if (level >= entity.Levels.Count) return;

        m_OnUpgradeHandler?.Invoke(level);

        ShowUIs(level);

        if (level == save.Level)
        {
            ShowEffect();
            GameManager.S.Player.IncreaseExp((int)(entity.Levels[level].Exp * data.RatioExp), this.transform);

            IncreaseLevel(1);

            m_OnUpgradeSuccess?.Invoke(level);

            DataManager.Save.General.IncreaseCountUpgrade(1);
        }

        Data.SumExp += (int)(entity.Levels[level].Exp * data.RatioExp);
    }

    public void ShowToast(string path)
    {
        m_ToastUp.Show(path);
    }

    private void ShowUIs(int level)
    {
        for (int i = 0; i < data.LevelUI.Count; i++)
        {
            if (data.LevelUI[i].index == level)
            {
                for (int j = 0; j < data.LevelUI[i].Showes.Count; j++)
                {
                    data.LevelUI[i].Showes[j].SetActive(true);
                }
                for (int j = 0; j < data.LevelUI[i].Hides.Count; j++)
                {
                    data.LevelUI[i].Hides[j].SetActive(false);
                }
            }
        }
    }

    private void ShowEffect()
    {
        PoolManager.S.Spawn(m_EfUpgrade, this.transform);
    }

    private void IncreaseLevel(int value)
    {
        save.IncreaseLevel(value);
        if (save.Level < entity.Levels.Count)
        {
            m_Upgrade.SetLevel(save.Level);
            int cost = entity.Levels[save.Level].Cost;
            save.SetCoinUp((int)(cost * (cost > 0 ? data.Ratio : 1)));
            m_Upgrade.SetCoin(save.CoinUp);
        }
        else
        {
            save.SetCoinUp(-2);
            m_Upgrade.SetMaxLevel();
        }
    }

    public void InteractShowUpEnter2D(Collider2D collision)
    {
        //Debug.Log("InteractShowUpEnter2D: " + collision.tag);
        if (collision.CompareTag("Player"))
        {
            m_Upgrade.gameObject.SetActive(true);
        }
    }

    public void InteractShowUpExit2D(Collider2D collision)
    {
        //Debug.Log("InteractShowUpExit2D: " + collision.tag);
        if (collision.CompareTag("Player"))
        {
            m_Upgrade.gameObject.SetActive(false);
        }
    }

    public void InteractUpgradeEnter2D(Collider2D collision)
    {
        //Debug.Log("InteractUpgradeEnter2D: " + collision.tag);
        if (collision.CompareTag("Player"))
        {
            m_Upgrade.Motion();
            m_Interact.Motion();

            if (save.Level < entity.Levels.Count)
            {
                Player player = collision.GetComponentInParent<Player>();
                if (handle.IsValid) Timing.KillCoroutines(handle);
                handle = Timing.RunCoroutine(_LootUpgrade(player));
            }
        }
    }

    int count = 2;
    private IEnumerator<float> _LootUpgrade(Player player)
    {
        decimal incre = save.CoinUp / Random.Range(7, 10);
        incre = Math.Min(Math.Max(4, incre), 50);

        if (save.CoinUp > 50 * 25)
        {
            incre = save.CoinUp / 25;
        }
        else if (save.CoinUp < 4 * 4)
        {
            incre = 4;
        }

        BagSave bag = DataManager.Save.Bag;

        while (true)
        {
            if (player.Data.Status == PlayerStatus.Idle)
            {
                if (save.CoinUp > 0 && bag.Currency[CurrencyType.Coin] > 0)
                {
                    incre = Math.Min(save.CoinUp, incre);
                    incre = Math.Min(bag.Currency[CurrencyType.Coin], incre);
                    incre = Math.Max(1, incre);

                    bag.SubtractCurrency(CurrencyType.Coin, (uint)incre);
                    SubtractCoinUp((int)incre);

                    Dummy coin = PoolManager.S.Spawn(ResourceManager.S.Dummy);
                    coin.Set(CurrencyType.Coin);
                    coin.transform.position = player.transform.position;
                    coin.transform.DOLocalJump(m_Upgrade.TranCoin.position, 2f, 1, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => PoolManager.S.Despawn(coin));
                    count++;
                    if (count % 3 == 0)
                    {
                        count = 0;
                    }
                    if (save.CoinUp <= 0)
                    {
                        Upgrade(save.Level);
                        break;
                    }
                    yield return Timing.WaitForSeconds(0.05f);
                }
                else if (save.CoinUp == -1)
                {
                    m_OnShowAds?.Invoke(save.Level);
                    break;
                }
                else break;
            }
            yield return Timing.WaitForSeconds(0.01f);
        }
        count = 2;
    }

    private void SubtractCoinUp(int value)
    {
        save.SubtractCoinUp(value);
        m_Upgrade.SetCoin(save.CoinUp);
    }

    public void InteractUpgradeExit2D(Collider2D collision)
    {
        //Debug.Log("InteractUpgradeEnter2D: " + collision.tag);
        if (collision.CompareTag("Player"))
        {
            m_Upgrade.UnMotion();
            m_Interact.UnMotion();
            if (handle.IsValid) Timing.KillCoroutines(handle);
        }
    }
}
