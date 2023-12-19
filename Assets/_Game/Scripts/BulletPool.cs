using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{

    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 50;
    [SerializeField] private int maxSize = 150;
    [SerializeField] private Bullet bullet;

    private static IObjectPool<Bullet> objectPool;

    private void Awake()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);
    }

    public static Bullet Get() {
        return objectPool.Get();
    }

    public static void Release(Bullet bullet) {
        objectPool.Release(bullet);
    }

    private void OnGetFromPool(Bullet Bullet)
    {
        Bullet.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet Bullet)
    {
        Bullet.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet Bullet)
    {
        Destroy(Bullet.gameObject);
    }

    private Bullet CreateBullet()
    {
        Bullet instance = Instantiate(bullet);
        return instance;
    }

}