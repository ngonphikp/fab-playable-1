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
public class SharedHuman : SharedVariable<Human>
{
    public static implicit operator SharedHuman(Human value) { return new SharedHuman { Value = value }; }
}

[System.Serializable]
public class Human : MonoBehaviour
{
    public HumanSave Save => save;
    public HumanEntity Entity => entity;
    public HumanData Data => data;

    [Header("Object")]
    [SerializeField] protected Transform m_Content;
    [SerializeField] private GameObject m_Graphic;
    [SerializeField] private GameObject m_SleepR;
    [SerializeField] private GameObject m_SleepL;
    [SerializeField] private GameObject m_Speed;

    [Header("UI")]
    [SerializeField] private EneryHuman m_Enery;
    [SerializeField] private ChatHuman m_Chat; 

    [Header("Properties")]
    [SerializeField] private HumanData data;
    private SkeletonAnimation sa;
    protected HumanSave save;

    private GroundAct groundAct;

    private HumanEntity entity;
    private HumanData originData;
    private BehaviorTree behavior;

    private CoroutineHandle handlePay;
    private Dictionary<ChatType, DateTime> lastTimeChat = new Dictionary<ChatType, DateTime>();
    private List<ChatType> canclesWhenShow = new List<ChatType>() { ChatType.SeeBoss, ChatType.FullStore, ChatType.SortCanteen };    

    private void Awake()
    {
        sa = m_Graphic.GetComponent<SkeletonAnimation>();
        sa.Initialize(false);

        behavior = GetComponent<BehaviorTree>();
    }

    private void OnDisable()
    {
        behavior.enabled = false;
    }

    private void OnDestroy()
    {
        if (handlePay.IsValid) Timing.KillCoroutines(handlePay);
    }

    public void Set(GroundAct ground, HumanData data, int idx)
    {
        this.groundAct = ground;
        this.data = data;
        
        this.originData = data.Clone();

        this.entity = DataManager.Data.Human.Dictionary[ground.ActData.HumanType];
        this.save = ground.ActSave.GetHuman(idx);
        if (save.Enery == -1) save.SetEnery(Entity.MaxEnery);        

        UpdateEneryUI();
        
        InitAI();
    }

    private void InitAI()
    {           
        behavior.enabled = true;
    }

    public void SetStatus(HumanStatus status)
    {
        if (this.data.Status == status) return;

        this.data.SetStatus(status);
        switch (data.Status)
        {
            #region Move Store
            case HumanStatus.MoveStore:
                {
                    string anim = "Move_Bao";
                    sa.AnimationState.SetAnimation(0, anim, true);
                }                
                break;
            case HumanStatus.Pay:
                sa.AnimationState.SetAnimation(0, "Idle", true);
                break;
            case HumanStatus.EndPay:
                
                break;
            #endregion
            #region Gain
            case HumanStatus.StartGain:
                sa.AnimationState.SetAnimation(0, "Move", true);
                break;
            case HumanStatus.GainCrop:
                sa.AnimationState.SetAnimation(0, "Move_ThuHoach", true);
                break;
            case HumanStatus.GainAnimal:
                sa.AnimationState.SetAnimation(0, "Idle_Back", true);
                break;
            case HumanStatus.EndGain:
                {
                    string anim = "Idle_Bao";
                    sa.AnimationState.SetAnimation(0, anim, true);
                }                
                break;
            #endregion
            #region Sow
            case HumanStatus.StartSow:
                sa.AnimationState.SetAnimation(0, "Move", true);
                break;
            case HumanStatus.Sow:
                
                break;
            case HumanStatus.EndSow:
                sa.AnimationState.SetAnimation(0, "Idle", true);
                save.SubstractEnery(Entity.EnerySow);
                UpdateEneryUI();
                break;
            #endregion
            #region Water
            case HumanStatus.StartWater:
                sa.AnimationState.SetAnimation(0, "Move", true);
                break;
            case HumanStatus.Water:
                sa.AnimationState.SetAnimation(0, "Move_Water", true);
                break;
            case HumanStatus.EndWater:
                sa.AnimationState.SetAnimation(0, "Idle", true);
                save.SubstractEnery(Entity.EneryWater);
                UpdateEneryUI();
                break;
            #endregion
            #region Wait
            case HumanStatus.StartWait:
                sa.AnimationState.SetAnimation(0, "Move", true);
                break;
            case HumanStatus.Wait:
                sa.AnimationState.SetAnimation(0, "Idle", true);
                break;
            case HumanStatus.EndWait:
                break;
            #endregion
            default:
                break;
        }
    }

    public Store GetStore()
    {
        return groundAct.Group.GetStore(this.transform, groundAct.ActData.ActType);
    }

    public Lands GetLands()
    {
        return groundAct.GetLands();
    }

    public Transform GetSleepPlace()
    {
        return groundAct.GetSleepPlace(this.transform);
    }

    public Transform GetWc()
    {
        return groundAct.Group.GetWc(this.transform);
    }

    public void UpdateEneryUI()
    {
        m_Enery.SetAmount(save.Enery * 1.0f / Entity.MaxEnery);
    }

    public bool IsSleep()
    {
        return sa.AnimationName == "Idle_Sleep";
    }

    public void SetDirection(Direction direction)
    {
        if (this.Data.Direction == direction) return;

        this.data.SetDirection(direction);

        switch (direction)
        {
            case Direction.Left:
                m_Content.localScale = new Vector3(-1, 1, 1);
                m_Enery.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                break;
            case Direction.Right:
                m_Content.localScale = new Vector3(1, 1, 1);
                m_Enery.gameObject.transform.localScale = new Vector3(1, 1, 1);
                break;
            default:
                break;
        }
    }
    
    public void Pay(Store store)
    {
        if (handlePay.IsValid) Timing.KillCoroutines(handlePay);
        handlePay = Timing.RunCoroutine(_PayAct(store));
    }

    private IEnumerator<float> _PayAct(Store store)
    {
        int number = Random.Range(2, 4);

        for (int i = 0; i < number; i++)
        {
            Dummy dm = PoolManager.S.Spawn(ResourceManager.S.Dummy);
            dm.Set(store.Type);
            dm.transform.position = transform.position;

            dm.transform.DOLocalJump(store.GetProgress().position, 2f, 1, 0.5f).SetEase(Ease.OutQuad).OnComplete(() => { PoolManager.S.Despawn(dm); });

            yield return Timing.WaitForSeconds(0.05f);
        }

        store.IncreaseAct(save.Act);
        save.SetAct(0);
        save.SubstractEnery(Entity.EneryCollect);
        UpdateEneryUI();

        yield break;
    }

    public void IncreaseSpeed(float speed)
    {
        data.IncreaseSpeed(speed);
    }

    public void IncreaseAct(int value)
    {
        save.IncreaseAct(value);
    }

    public void SubtractAct(int value)
    {
        save.SubstractAct(value);
    }

    public void ShowSpeed(bool isShow)
    {
        m_Speed.SetActive(isShow);
    }

    public void SetEatPlace(int index)
    {
        data.SetEat(index);
    }
}
