using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Player owner;
    private Vector3 target;
    private Weapon weapon;

    private void OnTriggerEnter(Collider other)
    {
        Player player = Cache.GetPlayer(other);
        if (player != owner)
        {
            owner.OnKill();
            player.OnDeath();
            Deactivate();    
        }
    }

    public void OnInit(Player owner, Vector3 target, Weapon weapon) {
        this.owner = owner;
        this.target = target;
        transform.rotation = Quaternion.Euler(Vector3.right * 90);
        this.weapon = weapon;
    }

    private void Update() {
        if (transform.position == target) Deactivate();
        transform.position = Vector3.MoveTowards(transform.position, target, Constants.WEAPON_SPEED * Time.deltaTime);
        transform.Rotate(0, Constants.rotateSpeed * Time.deltaTime, 0, Space.World);
    }

    private void Deactivate() {
        Destroy(gameObject);

        // Change Weapon cause the previously assigned weapon on the bullet become null (destroyed) 
        if (weapon) weapon.gameObject.SetActive(true);
    }

}