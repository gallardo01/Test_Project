using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" || other.tag == "Enemy") {
            other.GetComponent<Character>().OnHit(30f);
        }
    }
}
