using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cell prefab;
    [SerializeField] private Transform parent;    
   
    public List<Cell> Cells = new List<Cell>();
    private Vector2Int m_Column = new Vector2Int(-1, 1);
    private Vector2Int m_Row = new Vector2Int(-1, 1);

    public void Set(Lands lands, CropData data)
    {
        int count = 0;
        for (int i = m_Row.y; i >= m_Row.x; i--)
        {
            for (int j = m_Column.x; j <= m_Column.y; j++)
            {
                Timing.RunCoroutine(_SpawnCell(lands, data, j, i, 0.01f * count++));
            }
        }
    }

    public void IncreaseRowBottom(Lands lands, CropData data)
    {
        int count = 0;
        m_Row.y++;
        for (int i = m_Column.x; i <= m_Column.y; i++)
        {
            Timing.RunCoroutine(_SpawnCell(lands, data, i, m_Row.y, 0.01f * count++));
        }
    }

    public void IncreaseRowTop(Lands lands, CropData data)
    {
        int count = 0;
        m_Row.x--;
        for (int i = m_Column.x; i <= m_Column.y; i++)
        {
            Timing.RunCoroutine(_SpawnCell(lands, data, i, m_Row.x, 0.01f * count++));
        }
    }

    public void IncreaseColumnRight(Lands lands, CropData data)
    {
        int count = 0;
        m_Column.y++;
        for (int i = m_Row.x; i <= m_Row.y; i++)
        {
            Timing.RunCoroutine(_SpawnCell(lands, data, m_Column.y, i, 0.01f * count++));
        }
    }

    public void IncreaseColumnLeft(Lands lands, CropData data)
    {
        int count = 0;
        m_Column.x--;
        for (int i = m_Row.x; i <= m_Row.y; i++)
        {
            Timing.RunCoroutine(_SpawnCell(lands, data, m_Column.x, i, 0.01f * count++));
        }
    }

    private IEnumerator<float> _SpawnCell(Lands lands, CropData data, int i, int j, float time)
    {
        yield return Timing.WaitForSeconds(time);

        Cell cell = PoolManager.S.Spawn(prefab, parent);
        cell.SetCoord(new Cell.Coord(i, j));
        cell.SetPosition();
        if (lands) ((Land)cell).Set(lands, data.Clone());

        Cells.Add(cell);
    }
}
