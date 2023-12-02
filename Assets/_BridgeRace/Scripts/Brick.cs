using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Brick : ColorObject
{

    private IObjectPool<Brick> objectPool;

    public IObjectPool<Brick> ObjectPool { set => objectPool = value; }

    public void Deactivate()
    {
        objectPool.Release(this);
    }

    private void OnTriggerEnter(Collider other) {
        Deactivate();
    }
}