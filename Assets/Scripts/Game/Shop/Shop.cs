using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private List<SpriteRenderer> spThungs;
    [SerializeField] private List<SpriteRenderer> spKes;
    [SerializeField] private List<SpriteRenderer> spThungGos;

    [Header("Properties")]
    [SerializeField] private GameObject m_Circle;

    private GroundShop ground;

    public void Set(GroundShop ground)
    {
        this.ground = ground;
    }

    public void SetType(ActType type)
    {
        foreach (var sp in spThungs)
        {
            sp.sprite = ResourceManager.S.LoadSprite("Thungs/" + type.ToString(), false);
        }
        foreach (var sp in spKes)
        {
            sp.sprite = ResourceManager.S.LoadSprite("Kes/" + type.ToString(), false);
        }
        foreach (var sp in spThungGos)
        {
            sp.sprite = ResourceManager.S.LoadSprite("ThungGos/" + type.ToString(), false);
        }
    }

#if UNITY_EDITOR
    public void ToolSetType(ActType type)
    {
        foreach (var sp in spThungs)
        {
            sp.sprite = Resources.Load<Sprite>("Textures/Thungs/" + type.ToString());
        }
        foreach (var sp in spKes)
        {
            sp.sprite = Resources.Load<Sprite>("Textures/Kes/" + type.ToString());
        }
        foreach (var sp in spThungGos)
        {
            sp.sprite = Resources.Load<Sprite>("Textures/ThungGos/" + type.ToString());
        }
    }
#endif

    public Transform GetTransformCircle()
    {
        return m_Circle.transform;
    }
}
