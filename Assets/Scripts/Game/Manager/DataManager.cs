using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    public static DataSaveManager Save;
    public static DatabaseManager Data;
    public static IdenityManager Indenity;    

    public void Init()
    {
        DatabaseManager.S.Init(transform);
        IdenityManager.S.Init(transform);
        DataSaveManager.S.Init(transform);
    }
}
