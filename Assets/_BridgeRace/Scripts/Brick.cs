using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Brick : ColorObject
{

    [SerializeField] private Collider collider;

    private IObjectPool<Brick> objectPool;

    public IObjectPool<Brick> ObjectPool { set => objectPool = value; }
    public Collider Collider { get => collider; }

    public void Deactivate()
    {
        objectPool.Release(this);
    }

    private void OnTriggerEnter(Collider other) {
        if (colorType == other.GetComponent<ColorObject>().ColorType) {
            BrickSpawner.Ins.Consume(this);
            Deactivate();
        }
    }
}