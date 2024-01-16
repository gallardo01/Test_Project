using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkinItemData
{
    [SerializeField] public SkinItem prefab;
    [SerializeField] public Stat[] stats;
    [SerializeField] public int costs;
    [SerializeField] public Sprite sprite;

}

[Serializable]
public class Stat {
    [SerializeField] private int effectValues;
    [SerializeField] private EffectType effectTypes;
}

