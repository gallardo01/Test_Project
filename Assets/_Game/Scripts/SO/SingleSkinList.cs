using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hold one list of skin prefabs
[CreateAssetMenu(fileName = "SingleSkinList")]
public class SingleSkinList :ScriptableObject {
    [SerializeField] private SkinItemData[] items;

    public SkinItemData Get(int index) => items[index];
    public SkinItemData[] Items => items;
}