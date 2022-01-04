using DG.Tweening;
using MEC;
using Sirenix.OdinInspector;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public PlayerData Data => data;
    public PlayerSave Save => save;
    public PlayerEntity Entity => entity;

    [Header("Object")]
    [SerializeField] private Transform m_Content;
    [SerializeField] private GameObject m_Graphic;
    [SerializeField] private Joystick joystick;

    [SerializeField] private Transform m_See;
    [SerializeField] private Vector3 startSee = new Vector3(0.75f, 0, 0);
    [SerializeField] private Vector3 endSee = new Vector3(1.5f, 0, 0);
    [SerializeField] private float smooth = 0.1f;

    [SerializeField] private Transform posSkin;

    [Header("Properties")]
    [SerializeField] private PlayerData data;
    private SkeletonAnimation sa;
    private StackVisualAct stackVisualAct;

    //private List<Pet> pets = new List<Pet>();

    private PlayerSave save = new PlayerSave();
    private PlayerEntity entity;
    private PlayerData originData;

    [Header("UI")]
    [SerializeField] private ChatHuman m_Chat;

    private void Awake()
    {
        sa = m_Graphic.GetComponent<SkeletonAnimation>();
        sa.Initialize(false);

        stackVisualAct = GetComponent<StackVisualAct>();

        save = DataManager.Save.Player;

        originData = data.Clone();

        if (save.Position.z == -1) save.SetPosition(this.transform.position);
        else
        {
            Vector3 camPos = Camera.main.transform.position;
            camPos.x = save.Position.x;
            camPos.y = save.Position.y;
            Camera.main.transform.position = camPos;
            this.transform.position = save.Position;
        }             
    }

    public void Set()
    {
        GameScene.S.Profile.Set(this);

        entity = DataManager.Data.Player.List[save.Level];
    }

    private void SetStatus(PlayerStatus status)
    {
        if (this.data.Status == status) return;

        this.data.Status = status;

        switch (status)
        {
            case PlayerStatus.Idle:
                sa.AnimationState.SetAnimation(0, Random.Range(0, 99) % 5 != 0 ? "Idle" : "Idle_Special", true);
                break;
            case PlayerStatus.Move:
                sa.AnimationState.SetAnimation(0, "Move", true);
                
                //for (int i = 0; i < pets.Count; i++)
                //{
                //    pets[i].Data.SetMove(true);
                //}
                break;
            default:
                break;
        }
    }

    public void Update()
    {
        Vector2 direction = Vector2.up * joystick.Vertical + Vector2.right * joystick.Horizontal;
        bool none = direction.x == 0 && direction.y == 0;

        this.transform.Translate(direction.normalized * data.Speed * Time.deltaTime);

        if (direction.x != 0)
        {
            SetDirection(direction.x > 0 ? Direction.Right : Direction.Left);
        }

        SetStatus(none ? PlayerStatus.Idle : PlayerStatus.Move);
    }

    private void SetDirection(Direction direction)
    {
        if (this.data.Direction == direction) return;

        this.data.SetDirection(direction);

        switch (direction)
        {
            case Direction.Left:
                m_Content.localScale = new Vector3(-1, 1, 1);
                m_Chat?.Set(new Vector3(-1, 1, 1));
                break;
            case Direction.Right:
                m_Content.localScale = new Vector3(1, 1, 1);
                m_Chat?.Set(new Vector3(1, 1, 1));
                break;
            default:
                break;
        }

        m_See.localPosition = startSee;
        m_See.DOLocalMove(endSee, smooth).SetEase(Ease.OutQuad);      
    }

    public void ShowChat(int index)
    {
        string key = "Tutorial/Player_" + index;
        m_Chat?.Set(key, true, false);
    }

    public void ShowChatAutoHide(int index)
    {
        string key = "Tutorial/Player_" + index;
        m_Chat?.Set(key, true, true);
    }

    public void HideChat()
    {
        m_Chat?.gameObject.SetActive(false);
    }

    public void IncreaseAct(int amount, ActType actType, Vector3 startPos)
    {
        stackVisualAct.IncreaseAct(amount, actType, startPos);
    }

    public void SubstractAct(ActType type, Vector3 endPos, float ratio = 1.0f, Action complete = null)
    {
        Timing.RunCoroutine(stackVisualAct._SubstractAct(type, endPos, ratio, complete));
    }

    public void Collect()
    {
        stackVisualAct.Collect();
    }

    public void IncreaseExp(int value, Transform transform = null)
    {
        if (value <= 0) return;
        if (transform == null) transform = this.transform;

        if (save.Level == DataManager.Data.Player.List.Count - 1 && save.Exp == DataManager.Data.Player.List[DataManager.Data.Player.List.Count - 1].MaxExp) return;
        Timing.RunCoroutine(_IncreaseExp(value, transform));
    }

    public IEnumerator<float> _IncreaseExp(int value, Transform transform)
    {
        while (true)
        {
            if (save.Level > DataManager.Data.Player.List.Count - 1 || (save.Level == DataManager.Data.Player.List.Count - 1 && save.Exp == entity.MaxExp))
            {
                break;
            }

            if (save.Exp + value < entity.MaxExp)
            {
                save.IncreaseExp(value);
                GameScene.S.Profile.UpdateLevel();

                GameScene.S.Profile.EffectExp(transform);
                break;
            }
            else
            {
                int incre = entity.MaxExp - save.Exp;
                save.IncreaseExp(incre);
                GameScene.S.Profile.EffectExp(transform);

                if (save.Level < DataManager.Data.Player.List.Count - 1)
                {
                    save.IncreaseLevel(1);
                    entity = DataManager.Data.Player.List[save.Level];
                    save.SetExp(0);
                    value -= incre;
                }
                GameScene.S.Profile.UpdateLevel();
                yield return Timing.WaitForSeconds(0.2f);
            }
        }
    }
}
