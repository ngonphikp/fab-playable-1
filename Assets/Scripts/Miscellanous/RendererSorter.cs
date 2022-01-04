using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RendererSorter : MonoBehaviour
{
    public RendererMaker[] RendererMarkers; //Used for the sorting of groups.
    public string sortingLayerName; //For the sorting of groups.

    private void Update() //This will be called each time we change something in the scene.
    {
        if (Application.isPlaying)
        {
            return;
        }
        RendererMarkers = GetComponentsInChildren<RendererMaker>();
        for (int i = 0; i < RendererMarkers.Length; i++)
        {
            RendererMarkers[i].SetSortingLayer(i, sortingLayerName);
        }
    }
}

