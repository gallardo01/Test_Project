using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]

public class WeaponData : ScriptableObject
{

    public string NameWeapon;
    public string Description;
    public int Price;
    public float Speed;
    public float AttackRange;

}
