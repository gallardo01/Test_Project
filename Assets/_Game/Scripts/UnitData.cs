using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
[System.Serializable]
public class UnitData 
{
    public float hp;
    public int level;
    public string name;

    public UnitData() { }
    public UnitData(float hp, int level, string name) 
    {
        this.hp = hp;
        this.level = level;
        this.name = name;
    
    }

    public void SaveUnitData( UnitData unitdata)
    {
        
    }
}
