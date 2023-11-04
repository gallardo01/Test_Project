using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameController : MonoBehaviour
{
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private float spawnRate = 1f;
    [Space]
    [SerializeField] private MoveNode startNode;
    [SerializeField] private MoveNode endNode;

    private ObjectPool<EnemyController> _enemyPool;

    public static Action<int> OnScoreUpdate;
    public static int Score;

    private void Start()
    {
        _enemyPool = new ObjectPool<EnemyController>(CreateEnemy, GetEnemy, ReleaseEnemy);
        Score = 0;
        OnScoreUpdate.Invoke(0);
        
        InvokeRepeating(nameof(RepeatSpawning), 1f, spawnRate);
    }

    private void RepeatSpawning()
    {
        _enemyPool.Get();
    }
    
    private EnemyController CreateEnemy()
    {
        EnemyController controller = Instantiate(enemyPrefab, startNode.transform.position, Quaternion.identity);
        controller.OnInit(_enemyPool);
        controller.gameObject.SetActive(false);
        return controller;
    }

    private void GetEnemy(EnemyController enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.OnSpawn(new Route(startNode, endNode));
    }

    private void ReleaseEnemy(EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    public static void OnScore()
    {
        Score++;
        OnScoreUpdate.Invoke(Score);
    }
}
