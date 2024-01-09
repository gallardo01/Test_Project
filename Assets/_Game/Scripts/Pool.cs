using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool<T> where T : Component
{

    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 50;
    [SerializeField] private int maxSize = 150;

    private GameObject gameObject;
    private IObjectPool<T> pool;

    public Pool(GameObject gameObject) {
        pool = new ObjectPool<T>(Create,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);
        this.gameObject = gameObject;
    }

    public T Get()
    {
        return pool.Get();
    }

    private T Create()
    {
        return Object.Instantiate(gameObject).GetComponent<T>();
    }

    public void Release(T t)
    {
        pool.Release(t);
    }

    private void OnGetFromPool(T t)
    {
        t.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(T t)
    {
        t.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(T t)
    {
        Object.Destroy(t.gameObject);
    }

}