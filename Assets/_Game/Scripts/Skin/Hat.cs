using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : SkinItem
{
    [SerializeField]

    public override void Equip()
    {
        DoTheJob(LevelManager.Instance.MainCharacter.hat);
    }

}
