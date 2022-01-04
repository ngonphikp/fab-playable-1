using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTutorial : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private Transform arrow;

    private List<TutorialBox> childs = new List<TutorialBox>();

    private void Start()
    {
        if (content)
        {
            childs.Clear();
            foreach (Transform transform in content)
            {
                var box = transform.GetComponent<TutorialBox>();
                box.Set();
                childs.Add(box);
            }
        }
    }

    private void Update()
    {
        if (childs.Count > 0)
        {
            Transform find = null;
            for (int i = 0; i < childs.Count; i++)
            {
                if (childs[i].gameObject.activeSelf)
                {
                    find = childs[i].transform;
                    break;
                }
            }

            if (find)
            {
                Vector3 dir = (find.position + Vector3.down * 2f) - arrow.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
            }

            arrow.gameObject.SetActive(find);
        }
        else arrow.gameObject.SetActive(false);
    }
}
