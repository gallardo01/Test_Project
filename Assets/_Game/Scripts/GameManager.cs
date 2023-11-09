using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameController>
{
    [SerializeField] private GameObject[] respawnPoints;
    [SerializeField] private GameObject enemy;
    private int countEnemy = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(respawnEnemy());
    }

    IEnumerator respawnEnemy()
    {
        if (countEnemy < 5)
        {
            respawn(respawnPoints[Random.Range(0, 5)].transform);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(respawnEnemy());
    }
    private void respawn(Transform target)
    {
        countEnemy++;
        GameObject enemyClone = Instantiate(enemy, target);
        enemyClone.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    public void enemyDeath()
    {
        countEnemy--;
    }

    // private IEnumerator Delay1s(){
    //     yield return new WaitForSeconds(1f);
    //     StartCoroutine(Delay1s());
    // }
    // void Spawner()
    // {
    //     Instantiate(Enemy, DefenderSide.transform.position, Quaternion.identity);
    //     StartCoroutine(Delay1s()); // Tưởng nó sẽ dừng mãi nhưng nó lỗi
    //     Debug.Log("Spawn");
    // }
}
