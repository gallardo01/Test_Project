using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    
    private int heal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            heal = UnityEngine.Random.Range(250, 500);
            collision.GetComponent<Player>().OnHeal(heal);
            Invoke(nameof(DES), 0.2f);
        }
    }
    private void DES()
    {
        Destroy(gameObject);
    }
}
