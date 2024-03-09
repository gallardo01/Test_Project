using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skin equipped via Transform
public class TransformSkinItem : SkinItem
{
    public override void Equip(Player player) {
        Instantiate(gameObject, player.AvailableSkinPositions[(int) skinPosition]);
    }

    // Remove the current skin at the desired position, not care about what being equipped
    public override void UnEquip() {
        Destroy(gameObject);
    }
}
