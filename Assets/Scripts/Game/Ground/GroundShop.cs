using DG.Tweening;
using MEC;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GroundShop : Ground
{
    public GroundShopData ShopData => shopData;

    [Header("Object")]
    [SerializeField] private Shop m_Shop;
    [SerializeField] private InteractCircle m_Circle;
    [SerializeField] private UIBuyAct m_BuyAct;

    //[Header("UI")]

    [Header("Properties")]
    [SerializeField] private GroundShopData shopData;

    [Header("Event")]
    [SerializeField] private UnityEvent m_OnLootSuccess;

    private CoroutineHandle handleLoot;
    private int isLoot = -1;

    private GeneralSave general = new GeneralSave();

    protected override void Start()
    {
        base.Start();

        m_Shop.Set(this);
        m_BuyAct.gameObject.SetActive(false);
    }

    public override void Set(GroupGround group)
    {
        base.Set(group);

        //m_Shop.SetType(this.shopData.ActTypes[0]);
        m_BuyAct.AddItem(this.shopData.ActTypes);

        general = DataManager.Save.General;
    }

#if UNITY_EDITOR
    public void ToolLoadDataShop()
    {
        m_Shop.ToolSetType(this.shopData.ActTypes[0]);
    }
#endif

    public void InteractCircleEnter2D(Collider2D collision)
    {
        //Debug.Log("InteractCircleEnter2D: " + collision.tag);
        if (collision.CompareTag("Player"))
        {
            m_Circle.Motion();

            Player player = collision.GetComponentInParent<Player>();

            bool hasAct = false;
            for (int i = 0; i < shopData.ActTypes.Count; i++)
            {
                if (DataManager.Save.Bag.Act[shopData.ActTypes[i]] > 0)
                {
                    hasAct = true;
                    break;
                }
            }

            if (hasAct)
            {
                if (handleLoot.IsValid) Timing.KillCoroutines(handleLoot);
                handleLoot = Timing.RunCoroutine(_LootAct(player));
            }
        }
    }
    public void InteractCircleExit2D(Collider2D collision)
    {
        //Debug.Log("InteractCircleEnter2D: " + collision.tag);
        if (collision.CompareTag("Player"))
        {
            m_Circle.UnMotion();
            if (isLoot == -1)
            {
                if (handleLoot.IsValid) Timing.KillCoroutines(handleLoot);
            }
        }
    }

    private IEnumerator<float> _LootAct(Player player)
    {
        while (true)
        {
            if (player.Data.Status == PlayerStatus.Idle) break;
            //Debug.Log("Waiting Player Idle");
            yield return Timing.WaitForOneFrame;
        }

        isLoot = 0;
        var bag = DataManager.Save.Bag;
        uint sumCoin = 0;

        for (int i = 0; i < shopData.ActTypes.Count; i++)
        {
            ActType type = shopData.ActTypes[i];
            int count = bag.Act[type];
            if(count > 0)
            {
                isLoot++;

                player.SubstractAct(type, transform.position, 1, () =>
                {
                    bag.SubtractAct(type, count);
                    isLoot--;

                    sumCoin += (uint)(DataManager.Data.Act.GetCost(type) * count);
                });
            }
        }

        while (true)
        {
            if (isLoot == 0) break;
            //Debug.Log("Waiting Loot");
            yield return Timing.WaitForOneFrame;
        }

        uint incre = sumCoin / 10;
        uint remain = sumCoin % 10;

        for (int j = 0; j < 10; j++)
        {
            Dummy coin = PoolManager.S.Spawn(ResourceManager.S.Dummy);
            coin.Set(CurrencyType.Coin);
            coin.transform.position = transform.position + new Vector3(1.5f, 0.8f, 0f);
            coin.transform.parent = player.transform;
            coin.transform.DOLocalJump(Vector2.zero, 4f, 1, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                bag.IncreaseCurrency(CurrencyType.Coin, j == 0 ? (incre + remain) : incre);

                PoolManager.S.Despawn(coin);
            });
            yield return Timing.WaitForSeconds(0.05f);
        }
        isLoot = -1;

        m_OnLootSuccess?.Invoke();
        m_OnLootSuccess = null;

        yield break;
    }

    public void InteractShowBuyActEnter2D(Collider2D collision)
    {
        //Debug.Log("InteractShowBuyActEnter2D: " + collision.tag);
        if (collision.CompareTag("Player"))
        {
            m_BuyAct.gameObject.SetActive(true);
        }
    }

    public void InteractShowBuyActExit2D(Collider2D collision)
    {
        //Debug.Log("InteractShowActExit2D: " + collision.tag);
        if (collision.CompareTag("Player"))
        {
            m_BuyAct.gameObject.SetActive(false);
        }
    }
}
