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
    [SerializeField] private Transform Base, end;

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
            tmp.GetComponent<Bullet>().end = end;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }

        for (int i = 0; i < amountToPool * 5; i++)
        {
            tmp = Instantiate(playerObject);
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
        // for (int i = 0; i < 1 amountToPool * 5; i++)
        for (int i = 0; i < 1; i++)
        {
            if (!pooledPlayerObjects[i].activeInHierarchy)
            {
                return pooledPlayerObjects[i];
            }
        }
        return null;
    }
}
