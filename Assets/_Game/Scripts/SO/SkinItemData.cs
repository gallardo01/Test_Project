using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkinItemData
{
    [SerializeField] public SkinItem skinItem;
    [SerializeField] public Stat[] stats;
    [SerializeField] public int cost;
    [SerializeField] public Sprite sprite;
    [SerializeField] public string skinName;

}

[Serializable]
public class Stat {
    [SerializeField] private int effectValues;
    [SerializeField] private EffectType effectTypes;
}