using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnBrick : MonoBehaviour
{
    public GameObject Clone;
    public GameObject brickPref;
    private bool isCollect = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollect)
        {
            

            if (GameController.Instance.totalBrick > 0)
            {
                //Debug.Log("unCollect");
                isCollect = true;
                Instantiate(brickPref, transform.position, Quaternion.Euler(-90f, 0, 0)).transform.SetParent(levelManager.Instance.currentLevel.transform);
                other.GetComponent<Player>().removeBrick();
            }
            else
            {
                other.GetComponent<Player>().stop();
                GameController.Instance.changeState(GameState.Lose);
            }

           
            
        }
    }
}
