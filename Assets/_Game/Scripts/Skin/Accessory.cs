using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : SkinItem
{
    public override void Equip()
    {
        Equip(LevelManager.Instance.MainCharacter.accessory);
    }

    public override void UnEquip()
    {
        UnEquip(LevelManager.Instance.MainCharacter.accessory);
    }
}
