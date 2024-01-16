using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hold one list of skin prefabs
[CreateAssetMenu(fileName = "Single Skin List")]
public class SingleSkinList :ScriptableObject {
    [SerializeField] private SkinItemData[] items;

    public SkinItemData Get(int index) => items[index];
    public SkinItemData[] Items => items;
}


// Hold all lists of skin prefabs
[CreateAssetMenu(fileName = "Skin List")]
public class SkinList : ScriptableObject
{
    [SerializeField] private SingleSkinList[] skinLists;

    public SkinItemData GetSkin(int page, int index) => skinLists[page].Get(index);
    public SingleSkinList[] SkinLists => skinLists;
}
