using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialBox : MonoBehaviour
{
    public GameObject nextShowObj;
    public int myId;

    [SerializeField] private UnityEvent m_OnEnableSuccess;
    [SerializeField] private UnityEvent m_OnDisableSuccess;

    private void OnEnable()
    {
        m_OnEnableSuccess?.Invoke();
    }

    private void OnDisable()
    {
        m_OnDisableSuccess?.Invoke();
    }

    public void Set()
    {
        gameObject.SetActive(DataManager.Save.General.Tutorial == myId);
    }

    public void OnOpenSuccess()
    {
        DataManager.Save.General.Tutorial += 1;
        gameObject.SetActive(false);
        if (nextShowObj) nextShowObj.SetActive(true);
    }
}
