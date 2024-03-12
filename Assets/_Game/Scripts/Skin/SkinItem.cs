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
    // Can return void but return gameobject because transform skin need it
    // All players use the same renderer skin item in scriptable object
    public abstract SkinItem Equip(Player player, bool trying);

    public abstract void UnEquip();

}
