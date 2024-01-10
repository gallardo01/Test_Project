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
    [SerializeField] private float weaponSpeed;
    [SerializeField] private Collider collider;
    [SerializeField] private float rotateSpeed;

    private bool hit;
    private Vector3 target;
    private Vector3 basePosition;
    private Quaternion baseRotation;

    public float Range => range;
    public float Price => price;
    public string WeaponName => weaponName;
    public float AttackSpeed => attackSpeed;

    private Player owner;

    public void OnInit(Player owner)
    {
        this.owner = owner;
        basePosition = transform.localPosition;
        baseRotation = transform.localRotation;
        hit = false;
    }

    IEnumerator Fly()
    {
        hit = false;
        transform.rotation = Quaternion.Euler(Vector3.right * 90);
        transform.SetParent(null);
        while (transform.position != target && !hit)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, weaponSpeed * Time.deltaTime);
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
            yield return null;
        }
        ReturnToHand();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = Cache.GetPlayer(other);
        if (player != owner)
        {
            hit = true;
            owner.OnKill();
            player.OnDeath();
        }
    }

    public void Throw(Vector3 target)
    {
        collider.enabled = true;
        this.target = (target - transform.position).normalized * owner.attackRange + transform.position;
        StartCoroutine(Fly());
    }

    private void ReturnToHand()
    {
        transform.SetParent(owner.hand);
        transform.localPosition = basePosition;
        transform.localRotation = baseRotation;
        collider.enabled = false;
    }
}
