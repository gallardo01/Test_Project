using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class GameController : Singleton<GameController>
{
    [SerializeField] Transform[] pointEnemy;
    [SerializeField] GameObject Enemy;
    [SerializeField] private int kill = 0;
    private int coin;
    //public int[] spawnPoint = { 0, 0, 0, 0 };
    private int count = 0;
    
    private void Start()
    {
        UIManager.instance.setCoin(coin);
        UIManager.instance.setKill(kill);
        if(getStage() != 3)
        {
            StartCoroutine(respawnEnemy());
        }
        
     
    }
    private void Update()
    {
         if(PlayerPrefs.GetInt("Stage") == 1 )
        {
            if (kill == 2)
            {
                saveData(2);
                LoadNewScene(2);
            }
        }
    }
    IEnumerator respawnEnemy()
    {
        if(count < 4)
        {
            int i = Random.Range(0, 4);
            respawn(pointEnemy[i]);
            
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(respawnEnemy());
    }
    private void respawn( Transform target)
    {
        count++;
        Instantiate(Enemy, target.position, Quaternion.identity);

    }

    public void LoadNewScene(int level)
    {
        if(level == 1)
        {
            SceneManager.LoadScene("Level1");
            
        }
        if (level == 2)
        {
            SceneManager.LoadScene("Level2");
        }
        if (level == 3)
        {
            SceneManager.LoadScene("Level3");
        }
    }
    public void EnemyDead()
    {
        count--;
        kill++;
        UIManager.instance.setKill(kill); 
    }
    public void climbCoin()
    {
        coin++;
        UIManager.instance.setCoin(coin);
    }
    public int returnNumPoint(int num)
    {
        return num;
    }
    public void saveData(int stage)
    {
        PlayerPrefs.SetInt("Stage", stage);
    }
    public int getStage()
    {
        return PlayerPrefs.GetInt("Stage");
    }
}
