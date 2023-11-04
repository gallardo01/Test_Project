using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    [SerializeField] private bot botPrefabs;
    [SerializeField] private GameObject start;
    [SerializeField] private float timeSpawn;
    [SerializeField] private float countTime;
    // Start is called before the first frame update
    void Start()
    {
        countTime = 0;
        //Instantiate(botPrefabs, start.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if(countTime > timeSpawn)
        {
            Instantiate(botPrefabs,start.transform.position, Quaternion.identity);
            countTime = 0;
        }
    }
}
