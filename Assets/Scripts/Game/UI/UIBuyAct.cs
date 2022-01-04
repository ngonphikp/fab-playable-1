using MEC;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyAct : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private RectTransform title;

    [SerializeField] private UIBuyActItem prb;

    private RectTransform Frame { get { if (!frame)
            {
                frame = content.parent.GetComponent<RectTransform>();
                frame.sizeDelta = new Vector2(frame.sizeDelta.x, title.sizeDelta.y + frame.GetComponent<VerticalLayoutGroup>().padding.bottom + frame.GetComponent<VerticalLayoutGroup>().spacing);
            }
            return frame; } }
    private GridLayoutGroup Grid { get { if (!grid) grid = content.GetComponent<GridLayoutGroup>(); return grid; } }
    private RectTransform Rect { get { if (!rect) rect = content.GetComponent<RectTransform>(); return rect; } }

    private GridLayoutGroup grid;
    private RectTransform rect;
    private RectTransform frame;

    private int count = 0;

    public void AddItem(List<ActType> lst)
    {
        for (int i = 0; i < lst.Count; i++)
        {
            AddItem(lst[i]);
        }
    }

    public void AddItem(ActType type)
    {
        UIBuyActItem item = PoolManager.S.Spawn(prb, content);
        item.Set(type);
        count++;

        if (count == Grid.constraintCount + 1) // 5
        {
            Vector2 incre = new Vector2(Grid.cellSize.x, 0);
            title.sizeDelta += incre;
            Frame.sizeDelta += incre;

            incre = new Vector2(0, Grid.cellSize.y);
            Rect.sizeDelta -= incre;
            Frame.sizeDelta -= incre;
        }
        else if(count != Grid.constraintCount + 2 && count != Grid.constraintCount + 4) // =>  6 && 8
        {
            Vector2 incre = new Vector2(0, Grid.cellSize.y);
            Rect.sizeDelta += incre;
            Frame.sizeDelta += incre;
        }
    }
}
