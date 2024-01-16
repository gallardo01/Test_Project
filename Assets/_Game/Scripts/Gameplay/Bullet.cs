using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Player owner;
    private bool hit;
    private Vector3 target;
    private Vector3 basePosition;
    private Quaternion baseRotation;

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

    public void OnInit(Player owner, Vector3 target) {
        this
    }

    private void Update() {
        transform.rotation = Quaternion.Euler(Vector3.right * 90);
        while (transform.position != target && !hit)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Constants.WEAPON_SPEED * Time.deltaTime);
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0, Space.World);
        }
    }

}
