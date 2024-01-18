using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Inherit when new type require changes
public abstract class SkinItem : MonoBehaviour
{
    private Stat[] stats;

    // Call when wear, not when start game
    public void OnInit(Stat[] stats)
    {
        this.stats = stats;
    }

    public abstract void Equip();

    // Call inside Equip() to destroy the wearing skin and put the new one in 
    protected void DoTheJob(Transform position)
    {
        if (position.childCount > 0) 
        {
            Destroy(position.GetChild(0).gameObject); 
        }
        Instantiate(gameObject, position);
    }

    // Change player stats based on the item
    public virtual void ApplyEffect(Player player) {
        
    }

}
