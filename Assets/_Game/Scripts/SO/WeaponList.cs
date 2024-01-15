using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponList")]
public class WeaponList : ScriptableObject
{
    [SerializeField] private List<Weapon> weapons;

    public Weapon GetWeapon(int index) => weapons[index];

    public int Size => weapons.Count;
}
