using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pant : SkinItem
{
    [SerializeField] private Material material;

    public override void Equip()
    {
        LevelManager.Instance.MainCharacter.pant.material = material;
    }

    public override void UnEquip()
    {
        LevelManager.Instance.MainCharacter.pant.material = new Material(Shader.Find("Diffuse"));
    }
}
