using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AutoGrid : MonoBehaviour
{
    [SerializeField] private Vector2 space = new Vector2(0.3f, 0.24f);
    [SerializeField] private int col = 5;
    public SortingGroup [] listGroupSorting;

    private void Awake()
    {
        listGroupSorting = GetComponentsInChildren<SortingGroup>();      
    }

    public void UpdateGrid()
    {
        var allChild = transform.GetComponentsInChildren<VisualAct>();
       
        for (int i = 0; i < allChild.Length; i++)
        {
            int sortIndex = (listGroupSorting.Length-1) - i /30;
            if(sortIndex<0)
            {
                sortIndex = 0;
            }
            allChild[i].transform.localPosition = new Vector3(0f + i % col * space.x, 0f + i / col * space.y /*+ 0.01f * (i % col)*/);
            allChild[i].transform.parent = listGroupSorting[sortIndex].transform;
            allChild[i].transform.SetAsFirstSibling();
        }
        
    }
    public int count=0;
    public void AddElement(GameObject elementToAdd)
    {        
        int sortIndex = (listGroupSorting.Length - 1) - (count / 20); // 20 vi max sorting element la 32
        sortIndex = Mathf.Clamp(sortIndex, 0, listGroupSorting.Length - 1);

        elementToAdd.transform.localPosition = new Vector3(0f + count % col * space.x, 0f + count / col * space.y /*+ 0.01f * (i % col)*/);
        elementToAdd.transform.parent = listGroupSorting[sortIndex].transform;
        elementToAdd.transform.SetAsFirstSibling();
        count++;
    }
    public void SubjectElement()
    {
        count--;
    }
}
