using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float rotatationSpeed;

    private Player attacker;
    private Vector3 destination;
    private Weapon weapon;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        transform.Rotate(rotatationSpeed * Vector3.up * Time.deltaTime);
        if (Vector3.Distance(transform.position, destination) < 0.01f) Deactivate();
    }

    public void OnInit(Player attacker, Vector3 destination, Weapon weapon) {
        this.attacker = attacker;
        this.destination = destination;
        this.weapon = weapon;   
    }

    public void Deactivate() {
        weapon.gameObject.SetActive(true);
        BulletPool.Release(this);
    }

    private void OnTriggerEnter(Collider other) {
        Player player = Cache.GetPlayer(other);
        if (player == attacker) return;
        if (other.tag == Constants.PLAYER_TAG) {
            attacker.OnKill();
            player.OnDeath();
            Deactivate();
        }
    }
}
