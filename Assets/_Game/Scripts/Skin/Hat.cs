using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : SkinItem
{
    public override void Equip()
    {
        Equip(LevelManager.Instance.MainCharacter.hat);
    }

    public override void UnEquip()
    {
        UnEquip(LevelManager.Instance.MainCharacter.hat);
    }

}
