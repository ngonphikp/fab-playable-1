using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using NPS;

[System.Serializable]
public class Cell : MonoBehaviour
{
    [SerializeField] Vector2 m_Size;
    [SerializeField] Coord m_Coord = new Coord();

    public Coord GetCoord => m_Coord;

    public void SetCoord(Coord coord)
    {
        m_Coord.x = coord.x;
        m_Coord.y = coord.y;
    }

    public void SetPosition()
    {
        transform.localPosition = new Vector3(m_Size.x * m_Coord.x, -m_Size.y * m_Coord.y);
    }

    [System.Serializable]
    public class Coord
    {
        public int x;
        public int y;

        public Coord()
        {

        }

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Coord right => new Coord(x + 1, y);
        public Coord left => new Coord(x - 1, y);
        public Coord up => new Coord(x, y + 1);
        public Coord down => new Coord(x, y - 1);
    }
}
