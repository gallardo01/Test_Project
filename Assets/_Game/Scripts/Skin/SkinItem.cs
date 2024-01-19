using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Inherit when new type require changes
public abstract class SkinItem : MonoBehaviour
{
    
    protected string id;
    private Stat[] stats;

    // Call when wear, not when start game
    public void OnInit(Stat[] stats)
    {
        this.stats = stats;
    }

    public string ID => gameObject.GetInstanceID().ToString();

    public abstract void Equip();
    public abstract void UnEquip();

    // Call inside Equip() to destroy the wearing skin and put the new one in 
    protected void DoTheJob(Transform position)
    {
        if (position.childCount > 0)
        {
            Destroy(position.GetChild(0).gameObject); 
        }
        Instantiate(gameObject, position);
    }

    // Call inside UnEquip to destroy trying skin
    protected void UnEquip(Transform position) {
        if (position.childCount > 0)
        {
            Destroy(position.GetChild(0).gameObject); 
        }
    }

    // Change player stats based on the item
    public virtual void ApplyEffect(Player player) {
        
    }

}
