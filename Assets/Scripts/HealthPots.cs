using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPots : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Character>().Heal(20);
            Destroy(gameObject);
        }
    }
}
