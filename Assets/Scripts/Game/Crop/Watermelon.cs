using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : Crop
{
    public override void Set(Land land)
    {
        base.Set(land);
        Data.SetVisual(Random.Range(0, 99) % 2 == 0 ? CropVisual.A : CropVisual.B);
    }
}
