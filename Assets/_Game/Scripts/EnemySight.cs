using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Enemy enemy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            // Debug.Log("In");
            enemy.SetTarget(other.GetComponent<Character>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player"){
            // Debug.Log("Out");
            enemy.SetTarget(null);
        }
    }

}
