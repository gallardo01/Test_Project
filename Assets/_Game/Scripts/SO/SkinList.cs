using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hold all lists of skin prefabs
[CreateAssetMenu(fileName = "SkinList")]
public class SkinList : ScriptableObject
{
    [SerializeField] private SingleSkinList[] skinLists;

    public SkinItemData GetSkin(int page, int index) => skinLists[page].Get(index);
    public SingleSkinList[] SkinLists => skinLists;
}
