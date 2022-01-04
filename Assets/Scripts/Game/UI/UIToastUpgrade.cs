using MEC;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIToastUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject m_ObjContent;
    [SerializeField] private Image m_ImgContent;
    [SerializeField] private TextMeshProUGUI m_TextContent;

    private CoroutineHandle handle;

    public void Show(string path, float time = 2f)
    {
        Sprite sprite = ResourceManager.S.LoadSprite("Upgrade", path);
        m_TextContent.text = I2.Loc.LocalizationManager.GetTranslation("ToastUpgrade/" + path);
        Show(sprite, time);
    }

    public void Show(Sprite sprite, float time = 2f)
    {
        m_ImgContent.sprite = sprite;
        m_ObjContent.SetActive(true);
        if (handle.IsValid) Timing.KillCoroutines(handle);
        handle = Timing.RunCoroutine(_Show(time));
    }

    private void OnDestroy()
    {
        if (handle.IsValid) Timing.KillCoroutines(handle);
    }

    private IEnumerator<float> _Show(float time)
    {
        yield return Timing.WaitForSeconds(time);
        m_ObjContent.SetActive(false);
    }
}
