using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController2 prefabCharacter;
    public Bullet prefabBullet;
    public static GameController gameController;
    public int score = 0;
    void Awake()
    {
        gameController = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnMonster), 0f, 1f);
        Instantiate(prefabBullet, new Vector3(0,0,0), Quaternion.identity);
    }

    void SpawnMonster(){
        Instantiate(prefabCharacter, prefabCharacter.startPoint.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKey("a")){
        //     Debug.Log("A");
        // }
    }
}
