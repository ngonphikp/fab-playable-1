using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public static GameScene S = null;

    [Header("UI")]
    public UIBag Bag;
    public UIProfile Profile;

    private void Awake()
    {
        if (!S) S = this;
    }

    private void Start()
    {
        
    }
}
