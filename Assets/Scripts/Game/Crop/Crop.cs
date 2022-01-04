using MEC;
using Sirenix.OdinInspector;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Crop : MonoBehaviour
{
    public CropEntity Entity=> land.Entity;
    public CropData Data => land.Data;

    [Header("Object")]
    [SerializeField] private GameObject m_Graphic;

    [Header("Properties")]
    private SkeletonAnimation sa;
    private Land land;
    private CoroutineHandle handle;

    private void Awake()
    {
        sa = m_Graphic.GetComponent<SkeletonAnimation>();
        sa.Initialize(false);
        sa.AnimationState.Complete += OnAnimationComplete;
    }

    public virtual void Set(Land land)
    {
        this.land = land;
        Data.SetStatus(CropStatus.None);
    }

    private void OnDestroy()
    {
        sa.AnimationState.Complete -= OnAnimationComplete;
        if (handle.IsValid) Timing.KillCoroutines(handle);
    }

    public void SetStatus(CropStatus status)
    {
        if (this.Data.Status == status) return;

        this.Data.SetStatus(status);
        m_Graphic.SetActive(this.Data.Status != CropStatus.None);

        switch (status)
        {
            case CropStatus.None:
                break;
            case CropStatus.Sowing:
                Sow();
                break;
            case CropStatus.Seed:
                sa.AnimationState.SetAnimation(0, "Idle0", true);

                SetStatus(CropStatus.Germing);
                land.HasSowed();
                break;
            case CropStatus.Germing:
                Germ();
                break;
            case CropStatus.Germ:
                sa.AnimationState.SetAnimation(0, "Idle1", true);
                break;
            case CropStatus.Growing:
                Grow();
                break;
            case CropStatus.Immature:
                land.HasImmatured();
                break;
            case CropStatus.Maturing:
                Mature();
                break;
            case CropStatus.Mature:
                land.HasMatured();
                break;
            case CropStatus.Gaining:
                Gain();
                break;
            default:
                break;
        }
    }

    private void Sow()
    {
        SetStatus(CropStatus.Seed);
    }

    private void Germ()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
        handle = Timing.RunCoroutine(_Germ());
    }

    private IEnumerator<float> _Germ()
    {
        yield return Timing.WaitForSeconds(Entity.TimeGerm);
        sa.AnimationState.SetAnimation(0, "Upgrade_01", false);
    }

    private void Grow()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
        handle = Timing.RunCoroutine(_Grow());
    }

    private IEnumerator<float> _Grow()
    {
        yield return Timing.WaitForSeconds(Entity.TimeGrow);
        sa.AnimationState.SetAnimation(0, "Upgrade_12", false);

        SetStatus(CropStatus.Immature);
    }

    private void Mature()
    {
        string Upgrade_23 = "Upgrade_23";
        if (Data.Visual != CropVisual.None) Upgrade_23 += Data.Visual.ToString();
        SetStatus(CropStatus.Mature);
        sa.AnimationState.SetAnimation(0, Upgrade_23, false);
    }

    private void Gain()
    {
        string ThuHoach = "ThuHoach";
        if (Data.Visual != CropVisual.None) ThuHoach += "3" + Data.Visual.ToString();
        sa.AnimationState.SetAnimation(0, ThuHoach, false);
    }    

    private void OnAnimationComplete(Spine.TrackEntry trackEntry)
    {
        if (trackEntry.Animation.Name == "Upgrade_01")
        {
            SetStatus(CropStatus.Germ);
        }
        if (trackEntry.Animation.Name == "Upgrade_12")
        {
            sa.AnimationState.SetAnimation(0, "Idle2", true);
            //SetStatus(CropStatus.Immature);
        }
        if (trackEntry.Animation.Name == "Upgrade_23" || trackEntry.Animation.Name == "Upgrade_23A" || trackEntry.Animation.Name == "Upgrade_23B")
        {
            string Idle3 = "Idle3";
            if (Data.Visual != CropVisual.None) Idle3 += Data.Visual.ToString();

            sa.AnimationState.SetAnimation(0, Idle3, true);
        }
        if (trackEntry.Animation.Name == "ThuHoach" || trackEntry.Animation.Name == "ThuHoach3A" || trackEntry.Animation.Name == "ThuHoach3B")
        {
            SetStatus(CropStatus.None);
            land.HasGained();
        }
    }

    public void IncreaseQuantity(int value)
    {
        Data.IncreaseQuantity(value);
    }
}
