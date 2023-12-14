using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Brick : ColorObject
{

    [SerializeField] private Collider collider;

    private IObjectPool<Brick> objectPool;
    private Stage stage;

    public IObjectPool<Brick> ObjectPool { set => objectPool = value; }
    public Collider Collider { get => collider; }
    public Stage Stage { set => stage = value; }

    public void Deactivate()
    {
        objectPool.Release(this);
    }

    private void OnTriggerEnter(Collider other) {
        if (colorType == Cache.GetPlayer(other).ColorType || colorType == ColorType.Gray)
            Deactivate();
        if (colorType == Cache.GetPlayer(other).ColorType)
            stage.Consume(this);
    }
}