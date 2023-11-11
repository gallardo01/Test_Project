using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController2 : Singleton<GameController2>
{
    public List<GameObject> enemySpawning;
    public int sceneNumber = 0;
    private int countEnemy = 1;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawingEnemy), 0f, 10f);
        if (!PlayerPrefs.HasKey("Stage")){
            PlayerPrefs.SetInt("Stage", 0);
        }
        sceneNumber = PlayerPrefs.GetInt("Stage");
    }

    void SpawingEnemy(){
        int thisEnemy = Random.Range(0, enemySpawning.Count);
        if(enemySpawning[thisEnemy].activeSelf == false){
            enemySpawning[thisEnemy].SetActive(true);
            enemySpawning[thisEnemy].GetComponent<CapsuleCollider2D>().enabled = true;
            enemySpawning[thisEnemy].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            enemySpawning[thisEnemy].GetComponent<Enemy>().OnInit();
            countEnemy++;
        }
        if(countEnemy >= 3 && sceneNumber == 0){
            CancelInvoke(nameof(SpawingEnemy));
            sceneNumber++;
            PlayerPrefs.SetInt("Stage", 1);
            SceneManager.LoadScene("Scene2");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void UpdateEnemy(){

    // }

}
