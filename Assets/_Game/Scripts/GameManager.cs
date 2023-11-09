using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject[] respawnPoints;
    [SerializeField] private List<int> spawnedPoints;
    [SerializeField] private GameObject enemy;
    private int countEnemy = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(respawnEnemy());
        
    }

    IEnumerator respawnEnemy()
    {
        if (countEnemy < 5)
        {
            int randomPos = Random.Range(0, 5);
            
            respawn(respawnPoints[randomPos].transform);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(respawnEnemy());
    }
    private void respawn(Transform target)
    {
        countEnemy++;
        GameObject enemyClone =  Instantiate(enemy, target);
        enemyClone.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    public void enemyDeath()
    {
        countEnemy--;
    }
}
