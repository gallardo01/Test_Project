using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPool<T> where T : GameUnit
{
    private static List<GameObject> list = new List<GameObject>();
    private static Dictionary<GameObject, T> dict = new Dictionary<GameObject, T>();
    static GameObject prefab;
    static Transform parent;

    public static void OnInit(GameObject prefabs, Transform par,int num)
    {
        prefab = prefabs;
        parent = par;
        Preload(num);
    }
    public static void Preload(int num)
    {
        for (int i = 1; i < num; i ++) {

            GameObject go = GameObject.Instantiate(prefab, parent);
            list.Add(go);
            dict.Add(go, go.GetComponent<T>());
            go.SetActive(false);
        }
    }

    public static T Spawn( PoolType type)
    {
        GameObject go = null;

        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeInHierarchy && dict[list[i]].poolType == type)
            {
                go = list[i];
                break;
            }
        }
        go.SetActive(true);
        return dict[go];
    }

    public void Collect()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].activeInHierarchy)
            {
                list[i].SetActive(false);
            }
        }
    }

    public void Release()
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject.Destroy(list[i]);
        }

        list.Clear();
    }

}
