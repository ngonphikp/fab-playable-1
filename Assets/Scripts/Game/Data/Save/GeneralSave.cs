using System;
using System.Collections.Generic;

[System.Serializable]
public class GeneralSave
{
    public int CountUnlock = 0;
    public int CountUpgrade = 0;

    public int CountMission = 0;

    public DateTime Time = DateTime.UtcNow;

    public DateTime LateCancel = DateTime.UtcNow;
    public DateTime LateOffer3 = DateTime.UtcNow;

    public DateTime LastTimeOut = DateTime.UtcNow;
    public TimeSpan TimeOut
    {
        get
        {
            if ((DateTime.UtcNow - LastTimeOut).TotalMinutes <= 0)
            {
                return new TimeSpan(0, 0, 0, 0);
            }
            return DateTime.UtcNow - LastTimeOut;
        }
    }

    public float Sound = 1.0f;
    public float Music = 1.0f;

    public FpTypeUnlock FpTypeUnlock = new FpTypeUnlock();

    public int Tutorial = 0;

    public bool Ads = true;
    public delegate void OnAdsCallback();
    public event OnAdsCallback OnAdsEvent;

    public void IncreaseCountUnlock(int value)
    {
        CountUnlock += value;
    }

    public void IncreaseCountUpgrade(int value)
    {
        CountUpgrade += value;
    }

    public void IncreaseCountMission(int value)
    {
        CountMission += value;
    }

    public void SetTime(DateTime value)
    {
        Time = value;
    }

    public void SetLastTimeOut(DateTime value)
    {
        LastTimeOut = value;
    }

    public void SetSound(float value)
    {
        Sound = value;
    }

    public void SetMusic(float value)
    {
        Music = value;
    }

    public void SetAds(bool value)
    {
        Ads = value;

        if (OnAdsEvent != null) OnAdsEvent();
    }
}

[System.Serializable]
public class FpTypeUnlock
{
    public List<ActType> Data = new List<ActType>();

    private List<Action<ActType>> m_Listeners;

    public FpTypeUnlock()
    {
        m_Listeners = new List<Action<ActType>>();
    }

    public void Unlock(ActType value)
    {
        if (!Data.Contains(value)) Data.Add(value);

        InvokeUnlockFpType(value);
    }

    public void AddListener(Action<ActType> listener)
    {
        m_Listeners.Add(listener);
    }

    public void RemoveListener(Action<ActType> listener)
    {
        m_Listeners.Remove(listener);
    }

    private void InvokeUnlockFpType(ActType type)
    {
        for (int i = m_Listeners.Count - 1; i >= 0; i--)
        {
            m_Listeners[i].Invoke(type);
        }
    }
}
