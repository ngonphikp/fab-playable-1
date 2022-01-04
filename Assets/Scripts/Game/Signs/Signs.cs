using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signs : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_Signs;

    public void Set(ActType type)
    {
        m_Signs.sprite = ResourceManager.S.LoadSprite("Signs", type.ToString());
    }

#if UNITY_EDITOR
    public void ToolSet(ActType type)
    {
        m_Signs.sprite = Resources.Load<Sprite>("Textures/Signs/" + type.ToString());
    }
#endif
}
