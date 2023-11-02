using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawns : MonoBehaviour
{
    public GameObject SpawnObject;
    private GameObject Clone;
    public float speed;
    public float DelayTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnDelay());
        Debug.Log("Spawn");
    }

    private IEnumerator SpawnDelay()
    {
        Instantiate(SpawnObject, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);  
        StartCoroutine(SpawnDelay());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
