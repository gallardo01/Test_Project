using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    
    public GameObject[] Pools;
    public int currentStage;
    public GameObject brickPrefab;
    public int poolSize = 10;
    public float respawnTime;

    public Dictionary<string, List<GameObject>> BrickPools = new Dictionary<string, List<GameObject>>();
    public Dictionary<string, List<ColorType>> Stage = new Dictionary<string, List<ColorType>>();
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
            Stage.Add(pool.name, colors);
            for (int i = 1; i < 6; i++)
            {
                Stage[Pools[0].name].Add((ColorType)i);
            }
            
            Debug.Log(Stage[Pools[0].name].Count);
            Debug.Log(pool.name);
        }
        currentStage = 1;
    }
    




    public GameObject GetBrickFromPool(string pool)
    {
        foreach (GameObject brick in BrickPools[pool])
        {
            if (!brick.activeInHierarchy)
            {
                brick.SetActive(true);
                brick.GetComponent<Brick>().changColor(RandomColorInStage(pool));
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
        Pool pool = returnPool(name).GetComponent<Pool>();
        StartCoroutine(pool.RespawnBrick());
    } 
    
    public void OpenStage()
    {
        if(currentStage <= 3)
        {
            Pool pool = Pools[currentStage-1].GetComponent<Pool>();
            pool.ActiveAllBrick();
        }
    }

    public void addColortoStage(ColorType color, string pool)
    {
        Debug.Log("add");
        Stage[pool].Add(color);
        Debug.Log("added");
        Debug.Log(pool + Stage[pool][0]);
    }

    public void removeColortoStage(ColorType color)
    {

    }

    public ColorType RandomColorInStage(string pool)
    {
        return Stage[pool][Random.Range(0, Stage[pool].Count)];
    }
}
