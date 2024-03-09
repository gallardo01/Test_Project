using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Inherit when new type require changes
public abstract class SkinItem : MonoBehaviour
{
    protected Stat[] stats;

    [SerializeField] protected SkinPosition skinPosition;

    public SkinPosition SkinPosition => skinPosition;

    // Call when wear, not when start game
    public void OnInit(Stat[] stats)
    {
        this.stats = stats;
    }

    // Change player stats based on the item
    public virtual void ApplyEffect(Player player) {
        
    }

    public abstract void Equip(Player player);

    public abstract void UnEquip();

}
