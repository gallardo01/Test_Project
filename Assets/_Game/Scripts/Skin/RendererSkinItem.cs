using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skin equipped via renderer
public class RendererSkinItem : SkinItem
{
    [SerializeField] protected Material skinMaterial;
    protected Player player;
    
    public override void Equip(Player player)
    {
        player.SkinRenderers[(int) skinPosition].material = skinMaterial;
    }

    public override void UnEquip()
    {
        player.SkinRenderers[(int) skinPosition].material = Constants.Diffuse;
    }
}
