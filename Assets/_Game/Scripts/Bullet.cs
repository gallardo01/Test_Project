using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed;

    private Player attacker;
    private Vector3 destination;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, destination) < 0.01f) Deactivate();
    }

    public void OnInit(Player attacker, Vector3 destination) {
        this.attacker = attacker;
        this.destination = destination;
    }

    public void Deactivate() {
        BulletPool.Release(this);
    }

    private void OnTriggerEnter(Collider other) {
        Player player = Cache.GetPlayer(other);
        if (player == attacker) return;
        if (other.tag == Constants.PLAYER_TAG) {
            attacker.RemoveTarget(other);
            player.OnDeath();
            Deactivate();
        }
    }
}
