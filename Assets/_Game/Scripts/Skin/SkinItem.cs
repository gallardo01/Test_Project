using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Inherit when new type require changes
// This script is attached to the one on the scriptable object
// Equip function is called from the one in the scriptable to instantiate a copy from the sriptable
// UnEquip function is called from the one that is instantiated from Equip function
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

    // If trying, remove the old skin and apply the new one
    // If not trying, also update the new skin to equipped list
    
    // Equip is called on the scriptable object
    // Return the actual equipped item to stored on the player
    public abstract SkinItem Equip(Player player, bool trying);

    // Unequip is called on the actual equipped item
    public abstract void UnEquip();

}
