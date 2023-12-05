using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject pool;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            ObjectPooling.Ins.currentStage++;
            ObjectPooling.Ins.addColortoStage(player.colorType, pool.name);
            ObjectPooling.Ins.OpenStage();
            

        }
    }
}
