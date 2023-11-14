using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private Enemy enemy;
    [SerializeField] private int capacity;
    [SerializeField] int maxSize;

    private Dictionary<int, bool> occupiedPositions;
    private IObjectPool<Enemy> objectPool;
    private int usedEnemy = 0;

    private void Awake()
    {
        occupiedPositions = new Dictionary<int, bool>();
        objectPool = new ObjectPool<Enemy>(CreateEnemy, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, true, capacity, maxSize);
    }

    // invoked when creating an item to populate the object pool
    private Enemy CreateEnemy()
    {
        Enemy enemyInstance = Instantiate(enemy);
        enemyInstance.ObjectPool = objectPool;
        return enemyInstance;
    }

    // invoked when returning an item to the object pool
    private void OnReleaseToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        occupiedPositions[enemy.OccupiedIndex] = false;
        usedEnemy--;
        if (PlayerPrefs.GetInt("stage") == 1) Game2DController.Instance.UpdateProgress();
    }

    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        int index = UnityEngine.Random.Range(0, spawnPositions.Count);
        while (occupiedPositions.ContainsKey(index) && occupiedPositions[index]) {
            index = UnityEngine.Random.Range(0, spawnPositions.Count);
        }
        enemy.transform.position = spawnPositions[index].position;
        enemy.OccupiedIndex = index;
        occupiedPositions[index] = true;
        usedEnemy++;
    }

    // invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
    private void OnDestroyPooledObject(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    public void SpawnEnemy() {

        if (usedEnemy < spawnPositions.Count) objectPool.Get();

        Invoke(nameof(SpawnEnemy), 10);
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SpawnEnemy), 1);
        capacity = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
