using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    
    public GameObject[] Pools;
    public GameObject brickPrefab;
    public int poolSize = 10;
    public float respawnTime;

    public Dictionary<string, List<GameObject>> BrickPools = new Dictionary<string, List<GameObject>>();
    //List<GameObject> brickPool; 
    private void Awake()
    {
        foreach (var pool in Pools)
        {

            List<GameObject> obj = new List<GameObject>();
            for (int m = 0; m < poolSize; m++)
            {               
                GameObject brick = Instantiate(brickPrefab,pool.transform);
                brick.SetActive(false);
                obj.Add(brick);
            }
            BrickPools.Add(pool.name, obj);
            Debug.Log(pool.name);
        }
    }
    




    public GameObject GetBrickFromPool(string pool)
    {
        foreach (GameObject brick in BrickPools[pool])
        {
            if (!brick.activeInHierarchy)
            {
                brick.SetActive(true);
                brick.GetComponent<Brick>().changColor((ColorType)Random.Range(1,6));
                return brick;
            }
        }

        return null;
    }

    public void ReturnBrickToPool(GameObject brick)
    {
        brick.SetActive(false);  
    }
     public GameObject returnPool(string nameStage)
    {
        foreach (var pool in Pools)
        {
            if(pool.transform.name == nameStage)
            return pool;
        }
        return null;
    }
     public void respawn(string name)
    {
        Stage stage = returnPool(name).GetComponent<Stage>();
        StartCoroutine(stage.RespawnBrick());
    }  
}
