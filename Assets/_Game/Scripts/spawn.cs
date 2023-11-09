using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : Singleton<spawn>
{
    [SerializeField] Transform[] pointEnemy;
    [SerializeField] GameObject Enemy;

    public int[] spawnPoint = { 0, 0, 0, 0 };
    private int count = 0;
    private void Start()
    {
        StartCoroutine(respawnEnemy());
    }
    private void Update()
    {
       
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
    

    public void EnemyDead()
    {
        count--;
    }
    public int returnNumPoint(int num)
    {
        return num;
    }
}
