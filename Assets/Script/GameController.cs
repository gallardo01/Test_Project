using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerController2 prefabCharacter;
    public Bullet prefabBullet;
    public static GameController gameController;
    public Text scoreText;
    public int score = 0;
    void Awake()
    {
        gameController = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnMonster), 0f, 1f);
        // InvokeRepeating(nameof(SpawnBullet), 0f, 1f);
        SpawnBullet();
    }

    // public GameObject[] returnPath(){
    //     if(Random.Range(0,2) == 0){
    //         return path1;
    //     }
    //     return path2;
    // }

    void SpawnMonster(){
        Instantiate(prefabCharacter, prefabCharacter.startPoint.transform.position, Quaternion.identity);
    }

    void SpawnBullet(){
        Instantiate(prefabBullet, new Vector3(0,0,0), Quaternion.identity).InitBullet(Random.Range(0, 2));
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {score}";
        // if(Input.GetKey("a")){
        //     Debug.Log("A");
        // }
    }
}
