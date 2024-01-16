using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum EffectType {
    Range,
    MoveSpeed,
    Gold
}

// After create button, call init to create textmeshprougui, separate data from ui
public abstract class SkinItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI effectValue;
    [SerializeField] private TextMeshProUGUI effect;
    [SerializeField] private TextMeshProUGUI cost;

    // Init: Put data into textmeshprougui
}
