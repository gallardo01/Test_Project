using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public bool isCollect = false;
    
    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player") && !isCollect && GameController.Instance.isState(GameState.GamePlay))
        {
            //Debug.Log("clim");
            isCollect = true ;            
            other.GetComponent<Player>().addBrick();
            gameObject.SetActive(false);
        }
    }
}
