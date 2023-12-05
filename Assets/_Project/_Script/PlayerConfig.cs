using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player Config", fileName = "New Config")]
public class PlayerConfig : ScriptableObject
{
    public int diamondCount = 0;
    public int levelUnlocked = 1;
    
    public string skinSelected = "Yellow";
}
