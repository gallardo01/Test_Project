using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Brick : ColorControl
{
    private void Start()
    {
        changColor((ColorType)Random.Range(1,6));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player.colorType == ColorType)
            {
                Stage stage = transform.parent.GetComponent<Stage>();
                stage.emtyPoints.Add(transform.position);
                player.addBrick();                
                ObjectPooling.Ins.ReturnBrickToPool(gameObject);
                ObjectPooling.Ins.respawn(transform.parent.name);



            }
        }
    }
}
