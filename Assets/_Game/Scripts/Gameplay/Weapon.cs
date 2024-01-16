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
    [SerializeField] private float attackSpeed;
    [SerializeField] private float rotateSpeed;

    

    public float Range => range;
    public float Price => price;
    public string WeaponName => weaponName;
    public float AttackSpeed => attackSpeed;

    private Player owner;

    public void OnInit(Player owner)
    {
        this.owner = owner;
    }

    public void Throw(Vector3 target)
    {
        (target - transform.position).normalized * owner.attackRange + transform.position;
        StartCoroutine(Fly());
    }

    private void ReturnToHand()
    {
        transform.SetParent(owner.hand);
        transform.localPosition = basePosition;
        transform.localRotation = baseRotation;
    }
}
