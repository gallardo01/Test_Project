using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private Stage stage;

    private void OnTriggerEnter(Collider other) {
        if (stage.Level == 3){
            Debug.Log("Finish");
            return;
        }
        Player player = other.GetComponent<Player>();
        if (player.Level < stage.Level) {
            player.Level = stage.Level;
            BrickSpawner.Ins.StartLevel(stage.Level, other.GetComponent<Player>().ColorType);
        }
    }
}
