using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager S;

    private Dictionary<string, Sprite> dicSP = new Dictionary<string, Sprite>();

    private Dictionary<string, Crop> dicCrop = new Dictionary<string, Crop>();
    private Dictionary<string, Store> dicStore = new Dictionary<string, Store>();
    private Dictionary<string, Human> dicHuman = new Dictionary<string, Human>();

    public Dummy Dummy;
    public Dummy DummyUI;

    private void Awake()
    {
        if (!S) S = this;
    }

    public Sprite LoadSprite(string path, bool save = true)
    {
        return Load("Textures/" + path, dicSP, save);
    }

    public Sprite LoadSprite(string atlas, string name)
    {
        string path = atlas + "/" + name;
        return LoadSprite(path);
    }

    public Crop LoadCrop(string path)
    {
        return Load(path, dicCrop);
    }

    public Store LoadStore(string path)
    {
        return Load(path, dicStore);
    }

    public Human LoadHuman(string path)
    {
        return Load(path, dicHuman);
    }

    private T Load<T>(string path, Dictionary<string, T> dic, bool save = true) where T : Object
    {
        if (save)
        {
            if (!dic.ContainsKey(path))
            {
                T t = Resources.Load<T>(path);

                if (t) dic.Add(path, t);
                else Debug.LogError("Resource Null: " + path);
            }

            if (dic.ContainsKey(path)) return dic[path];
            return default;
        }
        else
        {
            return Resources.Load<T>(path);
        }
    }
}
