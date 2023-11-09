using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController2 : MonoBehaviour
{
    public List<GameObject> enemySpawning;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawingEnemy), 0f, 2f);
    }

    void SpawingEnemy(){
        int thisEnemy = Random.Range(0, enemySpawning.Count);
        if(enemySpawning[thisEnemy].activeSelf == false){
            enemySpawning[thisEnemy].SetActive(true);
            enemySpawning[thisEnemy].GetComponent<CapsuleCollider2D>().enabled = true;
            enemySpawning[thisEnemy].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            enemySpawning[thisEnemy].GetComponent<Enemy>().OnInit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
