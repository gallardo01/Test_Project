using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    //public GameObject brick;
    public bool isCollect = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollect)
        {
            Debug.Log("clim");
            isCollect = true ;            
            other.GetComponent<Player>().addBrick();
            gameObject.SetActive(false);
        }
    }
}
