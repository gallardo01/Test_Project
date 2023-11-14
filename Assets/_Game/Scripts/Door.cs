using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && other.GetComponent<Player>().HasKey()) {
            PlayerPrefs.SetInt("stage", 3);
            SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("stage"));
        }
    }
}
