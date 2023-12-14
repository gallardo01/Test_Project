using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : Singleton<ObjectPool>
{
    private IObjectPool<Brick> objectPool;

    public IObjectPool<Brick> Pool { get => objectPool; }

    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 50;
    [SerializeField] private int maxSize = 150;
    [SerializeField] private Brick brick;


    private void Awake()
    {
        objectPool = new ObjectPool<Brick>(CreateBrick,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);
    }

    private void OnGetFromPool(Brick brick)
    {
        brick.gameObject.SetActive(true);
        brick.Collider.enabled = true;
    }

    private void OnReleaseToPool(Brick brick)
    {
        brick.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Brick brick)
    {
        Destroy(brick.gameObject);
    }

    private Brick CreateBrick()
    {
        Brick instance = Instantiate(brick);
        instance.ObjectPool = objectPool;
        return instance;
    }

}
