using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class GameController : Singleton<GameController>
{
    [SerializeField] Transform[] pointEnemy;
    [SerializeField] GameObject Enemy;

    public int[] spawnPoint = { 0, 0, 0, 0 };
    private int count = 0;
    [SerializeField] private int kill = 0;
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(respawnEnemy());
        
        if (!PlayerPrefs.HasKey("Stage"))
        {
            PlayerPrefs.SetInt("Stage", 1);
        }
        
        
    }
    private void Update()
    {
         if(PlayerPrefs.GetInt("Stage") == 1 )
        {
            if(kill == 2)
            { 
                PlayerPrefs.SetInt("Stage", 2);
                LoadNewScene();
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

    public void LoadNewScene()
    {
        if(PlayerPrefs.GetInt("Stage") == 1)
        {
            SceneManager.LoadScene("Level1");
        }
        if (PlayerPrefs.GetInt("Stage") == 2)
        {
            SceneManager.LoadScene("Level2");
        }
        if (PlayerPrefs.GetInt("Stage") == 3)
        {
            SceneManager.LoadScene("Level3");
        }
    }
    public void EnemyDead()
    {
        count--;
    }
    public int returnNumPoint(int num)
    {
        return num;
    }
    public void addKill()
    {
        kill++;
    }
    //public void getStage()
    //{
    //    PlayerPrefs.GetInt("Stage");
    //}
}
