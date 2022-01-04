using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererMaker : MonoBehaviour
{
    // Marker used to give this gameobject's renderer the appropriate sorting group layer.

    MeshRenderer meshRenderer;  //It could be a mesh renderer.
    SpriteRenderer spriteRenderer; //Or it could be a sprite renderer.

    public void SetSortingLayer(int order, string sortingLayerName)
    {
        meshRenderer = GetComponent<MeshRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Then we sort based on the type of renderer from the void paramater.

        if (meshRenderer != null)
        {
            meshRenderer.sortingLayerName = sortingLayerName;
            meshRenderer.sortingOrder = order;
        }
        else if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = sortingLayerName;
            spriteRenderer.sortingOrder = order;
        }
    }
}