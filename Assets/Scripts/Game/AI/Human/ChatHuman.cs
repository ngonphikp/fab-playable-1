using I2.Loc;
using MEC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatHuman : MonoBehaviour
{
    [SerializeField] private Transform m_Content;
    [SerializeField] private TextMeshProUGUI m_TxtChat;
    private CoroutineHandle handle;

    private string key;
    private object[] data;

    private void Start()
    {
        LocalizationManager.OnLocalizeEvent += OnLocalize;
    }

    private void OnLocalize()
    {
        m_TxtChat.text = data.Length > 0 ? string.Format(LocalizationManager.GetTranslation(key), data) : LocalizationManager.GetTranslation(key);

        Timing.RunCoroutine(_Resize());
    }

    private IEnumerator<float> _Resize()
    {
        yield return Timing.WaitForOneFrame;
        m_Content.GetComponent<ContentSizeFitter>().enabled = false;
        m_Content.GetComponent<ContentSizeFitter>().enabled = true;

        yield return Timing.WaitForOneFrame;
        this.GetComponent<ContentSizeFitter>().enabled = false;
        this.GetComponent<ContentSizeFitter>().enabled = true;
    }

    private void OnDestroy()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
        LocalizationManager.OnLocalizeEvent -= OnLocalize;
    }

    public void Set(Vector3 scale)
    {
        m_Content.localScale = scale;
    }

    public void Set(string key, bool CDT = true, params object[] data)
    {
        this.key = key;
        this.data = data;

        m_TxtChat.text = data.Length > 0 ? string.Format(LocalizationManager.GetTranslation(key), data) : LocalizationManager.GetTranslation(key);

        if (handle.IsValid) Timing.KillCoroutines(handle);
        handle = Timing.RunCoroutine(_Chat(CDT));
    }

    private IEnumerator<float> _Chat(bool CDT)
    {        
        this.gameObject.SetActive(true);
        Timing.RunCoroutine(_Resize());

        if (CDT)
        {
            yield return Timing.WaitForSeconds(Random.Range(2f, 5f));
            this.gameObject.SetActive(false);
        }
    }
}
