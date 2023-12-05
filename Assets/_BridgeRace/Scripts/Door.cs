using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    [SerializeField] private Stage stage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (stage.Level == 3){
            Debug.Log("Finish");
            return;
        }
        PlayerController player = other.GetComponent<PlayerController>();
        if (player.Level < stage.Level) {
            player.Level = stage.Level;
            BrickSpawner.Instance.StartLevel(stage.Level, other.GetComponent<Player>().colorType);
        }
    }
}
