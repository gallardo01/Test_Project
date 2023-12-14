using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Stage stage;
    private void OnTriggerEnter(Collider other) {
        Player player = Cache.GetPlayer(other);
        player.Stage = stage;
        stage.StartLevel(player.ColorType);
    }
}
