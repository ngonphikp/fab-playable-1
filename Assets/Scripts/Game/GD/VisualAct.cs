using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualAct : MonoBehaviour
{
    public ActType Type => type;

    [SerializeField] private SpriteRenderer visual;
    private ActType type;

    public void Set(ActType type)
    {
        this.type = type;

        visual.sprite = ResourceManager.S.LoadSprite("Icons", type.ToString());

        visual.flipX = type == ActType.Corn;
    }

    public void Set(bool isVisual)
    {
        visual.enabled = isVisual;
    }
}
