using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : SkinItem
{
    [SerializeField] private Material skinMaterial;

    [SerializeField] private SkinItem[] itemsComeWithSkin;

    public override void Equip()
    {
        if (skinMaterial) LevelManager.Instance.MainCharacter.body.material = skinMaterial;

        foreach (SkinItem item in itemsComeWithSkin) {
            item.Equip();
        }
    }

    public override void UnEquip()
    {
        if (skinMaterial) LevelManager.Instance.MainCharacter.body.material = new Material(Shader.Find("Diffuse"));

        foreach (SkinItem item in itemsComeWithSkin) {
            item.UnEquip();
        }
    }
}
