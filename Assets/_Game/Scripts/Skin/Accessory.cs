using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : SkinItem
{
    public override void Equip()
    {
        DoTheJob(LevelManager.Instance.MainCharacter.accessory);
    }

    public override void UnEquip()
    {
        UnEquip(LevelManager.Instance.MainCharacter.accessory);
    }
}
