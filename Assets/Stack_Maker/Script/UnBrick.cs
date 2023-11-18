using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    public GameObject brickPref;
    private bool isCollect = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollect)
        {
            Debug.Log("unCollect");
            isCollect = true;
            Instantiate(brickPref, transform.position, Quaternion.Euler(-90f, 0, 0));
            other.GetComponent<Player>().removeBrick();
            
        }
    }
}
