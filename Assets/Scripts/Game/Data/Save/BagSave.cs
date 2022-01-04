using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;

[System.Serializable]
public class BagSave
{
    [ShowInInspector] public Dictionary<ActType, int> Act = new Dictionary<ActType, int>();
    [ShowInInspector] public Dictionary<CurrencyType, ulong> Currency = new Dictionary<CurrencyType, ulong>();
    [ShowInInspector] public Dictionary<int, uint> Skin = new Dictionary<int, uint>();
    [ShowInInspector] public Dictionary<int, uint> Pet = new Dictionary<int, uint>();

    private List<Action<object>> m_Listeners;

    public BagSave()
    {
        foreach (ActType act in (ActType[])Enum.GetValues(typeof(ActType)))
        {
            Act.Add(act, 0);
        }

        foreach (CurrencyType currency in (CurrencyType[])Enum.GetValues(typeof(CurrencyType)))
        {
            Currency.Add(currency, 0);
        }

        m_Listeners = new List<Action<object>>();

        Currency[CurrencyType.Coin] = 750;
    }

    public void IncreaseCurrency(CurrencyData currency)
    {
        IncreaseCurrency(currency.Type, currency.Value);
    }

    public void IncreaseCurrency(CurrencyType type, uint value)
    {
        Currency[type] += value;
        InvokeValueChanged();
    }

    public void SetCurrency(CurrencyType type, uint value)
    {
        Currency[type] = value;
        InvokeValueChanged();
    }

    public void SubtractCurrency(CurrencyType type, uint value)
    {
        Currency[type] -= value;
        InvokeValueChanged();
    }

    public void IncreaseAct(ActType type, int value)
    {
        Act[type] += value;
        InvokeValueChanged(type);
    }

    public void SubtractAct(ActType type, int value)
    {
        Act[type] = Math.Max(0, Act[type] -= value);
        InvokeValueChanged(type);
    }

    public void AddListener(Action<object> listener)
    {
        m_Listeners.Add(listener);
    }

    public void RemoveListener(Action<object> listener)
    {
        m_Listeners.Remove(listener);
    }

    private void InvokeValueChanged(object data = null)
    {
        for (int i = m_Listeners.Count - 1; i >= 0; i--)
        {
            m_Listeners[i].Invoke(data);
        }
    }
}
