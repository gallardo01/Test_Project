using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BotPool : MonoBehaviour
{

    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 50;
    [SerializeField] private int maxSize = 150;
    [SerializeField] private Bot bot;

    private static IObjectPool<Bot> objectPool;

    private void Awake()
    {
        objectPool = new ObjectPool<Bot>(CreateBullet,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);
    }

    public static Bot Get() {
        return objectPool.Get();
    }

    public static void Release(Bot bot) {
        objectPool.Release(bot);
    }

    private void OnGetFromPool(Player player)
    {
        player.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Player player)
    {
        player.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Player player)
    {
        Destroy(player.gameObject);
    }

    private Bot CreateBullet()
    {
        Bot instance = Instantiate(bot);
        return instance;
    }

}