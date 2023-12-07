using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    
    public GameObject[] Pools;
    public int currentStage;
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
            List<ColorType> colors = new List<ColorType>();
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
                return brick;
            }
        }

        return null;
    }

    public void ReturnBrickToPool(GameObject brick, Stage stage)
    {
        brick.SetActive(false);
    }


}
