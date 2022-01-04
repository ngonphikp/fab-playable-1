using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RemoteConfigSave
{
    [ShowInInspector] public Dictionary<string, string> Configs = new Dictionary<string, string>();

    public RemoteConfigSave()
    {
        Configs.Add(RemoteConfigKey.TimeCancel, "120");
    }
}

public class RemoteConfigKey
{
    public const string TimeCancel = "ab_time_cancel";
}
