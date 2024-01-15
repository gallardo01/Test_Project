using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pant : SkinItem
{
    public override void Equip()
    {
        DoTheJob(LevelManager.Instance.MainCharacter.pant);
    }
}
