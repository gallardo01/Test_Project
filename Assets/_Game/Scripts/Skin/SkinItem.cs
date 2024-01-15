using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType {
    Range,
    MoveSpeed,
    Gold
}

// Inherit when new type require changes
public abstract class SkinItem : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private EffectType effect;
    [SerializeField] private int cost;
    
    public float Value => value;
    public EffectType Effect => effect;
    public int Cost => cost;
    
    public abstract void Equip();

    protected void DoTheJob(Transform position) {
        if (position.childCount > 0) Destroy(position.GetChild(0).gameObject);
        Instantiate(gameObject, position);
    }
}
