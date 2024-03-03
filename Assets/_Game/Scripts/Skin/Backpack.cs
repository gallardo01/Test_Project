using UnityEngine;

public class Backpack : SkinItem
{
    public override void Equip()
    {
        Equip(LevelManager.Instance.MainCharacter.back);
    }

    public override void UnEquip()
    {
        UnEquip(LevelManager.Instance.MainCharacter.back);
    }
}