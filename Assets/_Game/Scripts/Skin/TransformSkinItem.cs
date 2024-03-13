using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skin equipped via Transform
public class TransformSkinItem : SkinItem
{
    public override SkinItem Equip(Player player, bool trying) {

        SkinItem si = Instantiate(this, player.AvailableSkinPositions[(int) skinPosition].transform);

        if (!trying) {
            player.EquippedSkin[(int) skinPosition] = si;
        }

        return si;
    }

    // Remove the current skin at the desired position, not care about what being equipped
    // Not destroy this gameobject (in the scriptable) but destroy the one created in Equip function
    public override void UnEquip() {
        Destroy(gameObject);
    }
}
