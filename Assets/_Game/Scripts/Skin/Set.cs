using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set : RendererSkinItem
{
    [SerializeField] private SkinItem[] itemsComeWithSkin;

    public override SkinItem Equip(Player player, bool trying)
    {
        // Equipped set 
        // Get the equipped set, then add the additional items to it so that destroy of the additional items are called on the instantiate one, not the scriptable one
        this.player = player;
        Set s = Instantiate(this);
        Cache.GetRenderer(player.AvailableSkinPositions[(int)skinPosition]).material = skinMaterial;

        if (!trying) {
            player.EquippedSkin[(int) skinPosition] = s;
        }

        // The items equipped with transform are attached to the right player
        for (int i = 0; i < itemsComeWithSkin.Length; i++) {
            s.itemsComeWithSkin[i] = itemsComeWithSkin[i].Equip(player, trying);
        }

        // Return a set with actual equipped item in the array, not the scriptable one
        return s;
    }

    public override void UnEquip()
    {
        // When trying skin, the state of itemsComeWithSkin will be removed, except for the state of the set
        // The state of the set is preserved so when the shop is closed, the state of the skin is applied, so does the other items that come with the set
        foreach (SkinItem item in itemsComeWithSkin) {
            item.UnEquip();
        }

        base.UnEquip();

        Destroy(gameObject);
    }
}
