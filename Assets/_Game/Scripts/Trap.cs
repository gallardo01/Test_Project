using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
//using Random = UnityEngine.Random;


public class Trap : MonoBehaviour
{
    private float time;
    private int damage;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            damage = UnityEngine.Random.Range(25, 50);

            time -= Time.deltaTime;
            if (time <= 0)
            {
                collision.GetComponent<Player>().OnHit(damage);
                time = 0.8f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        time = 0;
    }
}
