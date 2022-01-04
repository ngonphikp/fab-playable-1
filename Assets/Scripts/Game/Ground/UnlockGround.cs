using MEC;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

public class UnlockGround : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject frameLock;
    [SerializeField] private InteractCircle interactOpen;
    [SerializeField] private GameObject bound;
    [SerializeField] protected GameObject m_EfUnlock;

    [Header("UI")]
    [SerializeField] private UIUnlock unlock;
    [SerializeField] private UIRequire require;

    [Header("Properties")]
    private CoroutineHandle handle;

    [Header("Event")]
    [SerializeField] public UnityEvent m_OnUnlockSuccess;

    private GroundSave save;
    private GroundData data;

    private void Start()
    {
        
    }

    public void Init(GroundSave save, GroundData data)
    {
        this.save = save;
        this.data = data;
        if (save.IsLock) CheckOpen();
    }

    public void LoadData()
    {
        unlock.SetCoin(save.Coin);
        require.SetLevel(data.Level);
        SetLock(save.IsLock);
    }

#if UNITY_EDITOR
    [Button]
    public void ToolLoadData()
    {
        data = GetComponent<Ground>().Data;

        unlock.SetCoin(data.Coin);
        require.SetLevel(data.Level);

        ToolSetLock(data.IsLock);
    }
#endif

    public void CheckOpen()
    {
        List<Ground> newConditions = new List<Ground>();
        for (int i = 0; i < data.Conditions.Count; i++)
        {
            if (data.Conditions[i].Save.IsLock)
            {
                newConditions.Add(data.Conditions[i]);
            }
        }

        data.Conditions = newConditions;
        this.save.SetLock(!(this.save.Coin == 0 && data.Conditions.Count == 0));
        SetLock(this.save.IsLock);
    }

    public void CheckRequire()
    {
        if (!this.save.IsLock) return;
        int level = DataManager.Save ? DataManager.Save.Player.Level : 0;
        require.gameObject.SetActive(data.Level > level && data.Conditions.Count == 0);
        unlock.gameObject.SetActive(data.Level <= level && data.Conditions.Count == 0);
        frameLock.SetActive(data.Conditions.Count == 0);
        interactOpen.gameObject.SetActive(data.Conditions.Count == 0);
    }

    public void SetLock(bool isLock)
    {
        content.SetActive(!isLock);

        bound.SetActive(isLock);

        if (isLock)
        {
            CheckRequire();
        }
        else
        {
            Destroy(unlock);
            Destroy(frameLock);
            Destroy(bound);            

            data.SumExp += data.Exp;
        }
    }

#if UNITY_EDITOR
    [Button]
    public void ToolSetLock(bool isLock)
    {
        content.SetActive(!isLock);

        bound.SetActive(isLock);

        if (isLock)
        {
            require.gameObject.SetActive(false);
            unlock.gameObject.SetActive(true);
            frameLock.SetActive(true);
            interactOpen.gameObject.SetActive(true);
        }
        else
        {
            require.gameObject.SetActive(false);
            interactOpen.gameObject.SetActive(false);
            unlock.gameObject.SetActive(false);
            frameLock.SetActive(false);
        }
    }
#endif

    public void InteractOpenEnter2D(Collider2D collision)
    {
        //Debug.Log("InteractOpenEnter2D: " + collision.tag);
        if (collision.CompareTag("Player"))
        {
            interactOpen.Motion();
            if (DataManager.Save.Player.Level >= data.Level)
            {
                unlock.Motion();

                Player player = collision.GetComponentInParent<Player>();
                if (handle.IsValid) Timing.KillCoroutines(handle);
                handle = Timing.RunCoroutine(_LootOpen(player));
            }
            else require.Motion();            
        }
    }

    int count = 2;
    private IEnumerator<float> _LootOpen(Player player)
    {
        decimal incre = this.save.Coin / Random.Range(7, 10);
        incre = Math.Min(Math.Max(4, incre), 50);

        if (this.save.Coin > 50 * 25)
        {
            incre = this.save.Coin / 25;
        }
        else if (this.save.Coin < 4 * 4)
        {
            incre = 4;
        }

        BagSave bag = DataManager.Save.Bag;

        while (true)
        {
            if (player.Data.Status == PlayerStatus.Idle)
            {
                if (this.save.Coin > 0 && bag.Currency[CurrencyType.Coin] > 0)
                {
                    incre = Math.Min(this.save.Coin, incre);
                    incre = Math.Min(bag.Currency[CurrencyType.Coin], incre);
                    incre = Math.Max(1, incre);

                    bag.SubtractCurrency(CurrencyType.Coin, (uint)incre);
                    this.save.SubtractCoin((int)incre);
                    unlock.SetCoin(this.save.Coin);

                    Dummy coin = PoolManager.S.Spawn(ResourceManager.S.Dummy);
                    coin.Set(CurrencyType.Coin);
                    coin.transform.position = player.transform.position;
                    coin.transform.DOLocalJump(unlock.TranCoin.position, 2f, 1, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => PoolManager.S.Despawn(coin));
                    count++;
                    if (count % 3 == 0)
                    {
                        count = 0;
                    }
                    if (this.save.Coin <= 0)
                    {
                        OpenSuccess();
                        break;
                    }
                    yield return Timing.WaitForSeconds(0.05f);
                }
                else if (this.save.Coin == -1)
                {
                    OpenSuccess();
                    GameManager.S.isReward = true;
                    break;
                }
                else break;
            }
            yield return Timing.WaitForSeconds(0.01f);
        }
        count = 2;
    }

    private void OpenSuccess()
    {
        this.save.SetLock(false);
        SetLock(false);

        PoolManager.S.Spawn(m_EfUnlock, this.transform);
        GameManager.S.Player.IncreaseExp(data.Exp, this.transform);

        m_OnUnlockSuccess?.Invoke();

        GameManager.S.OpenSucess();
        DataManager.Save.General.IncreaseCountUnlock(1);
    }

    public void InteractOpenExit2D(Collider2D collision)
    {
        //Debug.Log("InteractOpenExit2D: " + collision.tag);
        if (collision.CompareTag("Player"))
        {
            interactOpen.UnMotion();

            if (DataManager.Save.Player.Level >= data.Level)
            {
                unlock.UnMotion();
                if (handle.IsValid) Timing.KillCoroutines(handle);
            }
            else require.UnMotion();
        }
    }
}
