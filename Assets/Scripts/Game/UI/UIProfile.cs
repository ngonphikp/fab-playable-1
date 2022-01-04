using MEC;
using NPS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIProfile : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI m_TxtLevel;
    [SerializeField] private TextMeshProUGUI m_TxtProgess;
    [SerializeField] private ProgressBarPro m_ProgressBar;

    private Player player;

    public void Set(Player player)
    {
        this.player = player;
        UpdateLevel(0);
    }

    public void UpdateLevel(float time = 1.75f)
    {
        Timing.RunCoroutine(_UpdateLevel(time));
    }

    private IEnumerator<float> _UpdateLevel(float time)
    {
        yield return Timing.WaitForSeconds(time);

        m_TxtLevel.text = (player.Save.Level + 1).ToString();
        m_TxtProgess.text = MathHelper.ScoreShow(player.Save.Exp) + "/" + MathHelper.ScoreShow(player.Entity.MaxExp);
        m_ProgressBar.SetValue(player.Save.Exp * 1.0f / player.Entity.MaxExp);
    }

    public void EffectExp(Transform transform)
    {
        for (int i = 0; i < Random.Range(8, 12); i++)
        {
            Dummy star = PoolManager.S.Spawn(ResourceManager.S.DummyUI);
            star.Set("star");
            star.Loot(transform, m_TxtLevel.gameObject.transform, true, () =>
            {
            });
        }
    }
}
