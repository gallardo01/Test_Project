using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : RendererSkinItem
{
    [SerializeField] private SkinItem[] itemsComeWithSkin;
    private Player player;

    public override void Equip(Player player)
    {
        base.Equip(player);

        this.player = player;

        foreach (SkinItem item in itemsComeWithSkin) {
            player.Equip(item);
        }
    }

    public override void UnEquip()
    {
        base.UnEquip();

        // When trying skin, the state of itemsComeWithSkin will be removed, except for the state of the set
        // The state of the set is preserved so when the shop is closed, the state of the skin is applied, so does the other items that come with the set
        foreach (SkinItem item in itemsComeWithSkin) {
            player.UnEquip(item);
        }
    }
}
