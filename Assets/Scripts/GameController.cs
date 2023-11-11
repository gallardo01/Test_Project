using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private GameObject[] respawnPoint;
    [SerializeField] private GameObject enemyPrefab;
    private int enemyCount = 0;

    void Start()
    {
        StartCoroutine(respawnEnemy());

        if (!PlayerPrefs.HasKey("Stage"))
        {
            PlayerPrefs.SetInt("Stage", 1);
        }

        int stage;
        stage = PlayerPrefs.GetInt("Stage");

        if (stage == 2)
        {

        }
    }

    IEnumerator  respawnEnemy()
    {
        if (enemyCount < 2)
        {
            respawn(respawnPoint[Random.Range(0, respawnPoint.Length)].transform);
        }
        
        yield return new WaitForSeconds(5f);
        StartCoroutine(respawnEnemy());
    }

    private void respawn(Transform target)
    {
        enemyCount++;
        GameObject clone = Instantiate(enemyPrefab, target.position, Quaternion.identity);
    }

    public void decreaseEnemyCount()
    {
        enemyCount--;
    }
}
