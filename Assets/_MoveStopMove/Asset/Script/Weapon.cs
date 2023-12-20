using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WEAPON
{
    Arrow,
    Axe,
    Boomerang,
    Candy,
    Hammer,
    Knife,
    Uzi,
    Z
}
public class Weapon : MonoBehaviour
{
    public List<WEAPON> LISTWEAPON = new List<WEAPON>();
    public List<GameObject> LISTweapons = new List<GameObject>();
}
