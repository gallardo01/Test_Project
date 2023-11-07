using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<Transform> spawnPositions;
    [SerializeField] GameObject Enemy;

    private Dictionary<int, bool> occupiedPositions;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SpawnEnemy), 1);
    }

    private void SpawnEnemy() {
        int index;

        do {
            index = UnityEngine.Random.Range(0, spawnPositions.Count);
        } while (occupiedPositions.ContainsKey(index) && occupiedPositions[index]);

        Instantiate(Enemy, spawnPositions[index]);
        occupiedPositions[index] = true;

        Invoke(nameof(SpawnEnemy), 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
