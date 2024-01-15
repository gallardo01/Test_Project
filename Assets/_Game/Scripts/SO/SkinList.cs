using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hold one list of skin prefabs
[CreateAssetMenu(fileName = "Single Skin List")]
public class SingleSkinList :ScriptableObject {
    [SerializeField] private SkinItem[] items;

    public SkinItem Get(int index) => items[index];
}


// Hold all lists of skin prefabs
[CreateAssetMenu(fileName = "Skin List")]
public class SkinList : ScriptableObject
{
    [SerializeField] private SingleSkinList[] skinLists;

    public SkinItem GetSkin(int page, int index) => skinLists[page].Get(index);
}
