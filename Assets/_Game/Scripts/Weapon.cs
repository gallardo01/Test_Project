using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum WeaponType {
    Axe = 0,
    DoubleAxe = 1,
    Boomerang = 2,
}

public class Weapon : MonoBehaviour
{

    [SerializeField] private float range;
    [SerializeField] private float price;
    [SerializeField] private float speed;

    public float Range => range;
    public float Price => price;
    public float Speed => speed;

    private Player owner;

    public void OnInit(Player owner) {
        this.owner = owner;
    }

    public void Throw(Vector3 target) {
        Bullet bullet = BulletPool.Get();
        bullet.transform.position = owner.transform.position;
        bullet.OnInit(owner, target, this);

        gameObject.SetActive(false);
    }
}
