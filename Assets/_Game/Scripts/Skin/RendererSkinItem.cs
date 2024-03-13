using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skin equipped via renderer
public class RendererSkinItem : SkinItem
{
    [SerializeField] protected Material skinMaterial;
    public Player player;

    // Changing this type of skin does not require UnEquip so dont need to have separate player reference for each item
    // Assigning player variable only used for changing main character skin in shop
    public override SkinItem Equip(Player player, bool trying)
    {
        this.player = player;
        Cache.GetRenderer(player.AvailableSkinPositions[(int)skinPosition]).material = skinMaterial;

        if (!trying) {
            player.EquippedSkin[(int) skinPosition] = this;
        }

        return this;
    }

    public override void UnEquip()
    {
        // When trying, skin state might be null
        if (player.AvailableSkinPositions[(int)skinPosition]) Cache.GetRenderer(player.AvailableSkinPositions[(int)skinPosition]).material = Constants.Diffuse;
    }
}
