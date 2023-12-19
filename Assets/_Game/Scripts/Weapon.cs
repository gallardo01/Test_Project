using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{

    private Player owner;

    private void Start() {
    }

    public void OnInit(Player owner) {
        this.owner = owner;
    }

    public void Throw(Vector3 target) {
        Bullet bullet = BulletPool.Get();
        bullet.transform.position = transform.position;
        bullet.OnInit(owner, target);
    }
}
