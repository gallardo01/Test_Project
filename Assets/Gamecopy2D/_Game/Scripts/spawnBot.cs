using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBot : MonoBehaviour
{
    [SerializeField] private GameObject botPrefab;
    // Update is called once per frame
    private void Start()
    {
        Instantiate(botPrefab, transform.position, Quaternion.identity);                                                                                                                            
    }
    public void SpawnEnemy()
    {
        Instantiate(botPrefab, transform.position, Quaternion.identity);
    }
}
