using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandWeapon : SkinItem
{
    public override void Equip()
    {
        Equip(LevelManager.Instance.MainCharacter.leftHand);
    }

    public override void UnEquip()
    {
        UnEquip(LevelManager.Instance.MainCharacter.leftHand);
    }
}
