using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public enum WeaponType
{
    Axe = 0,
    DoubleAxe = 1,
    Boomerang = 2,
}

public class Weapon : MonoBehaviour
{

    [SerializeField] private float range;
    [SerializeField] private string weaponName;
    [SerializeField] private float price;
    [SerializeField] private GameObject bullet;

    public float Range => range;
    public float Price => price;
    public string WeaponName => weaponName;

    private Player owner;

    public void OnInit(Player owner)
    {
        this.owner = owner;
    }

    public void Throw(Vector3 target)
    {
        gameObject.SetActive(false);
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().OnInit(owner, target, this);
    }
}
