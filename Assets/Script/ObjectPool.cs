using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects, pooledPlayerObjects;
    public GameObject objectToPool, playerObject;
    public int amountToPool;
    [SerializeField] private Transform Base, End, Four;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.GetComponent<Bullet>().Base = Base;
            tmp.GetComponent<Bullet>().End = End;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }

        for (int i = 0; i < amountToPool * 5; i++)
        {
            tmp = Instantiate(playerObject);
            tmp.GetComponent<PlayerController>().End = End;
            tmp.GetComponent<PlayerController>().Four = Four;
            tmp.SetActive(false);
            pooledPlayerObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public GameObject GetPlayer()
    {
        for (int i = 0; i < amountToPool * 3; i++)
        {
            if (!pooledPlayerObjects[i].activeInHierarchy)
            {
                return pooledPlayerObjects[i];
            }
        }
        return null;
        
    }
    
    


}
