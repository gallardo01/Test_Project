using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Stacker : MonoBehaviour
{
    [SerializeField] private Transform mesh;
    [SerializeField] private Transform brickPrefab;
    [SerializeField] private Transform brickRoot;
    [SerializeField] private int numberOfBrick;
    [SerializeField] private float brickHeight;

    private ObjectPool<Transform> _brickPool;
    private readonly List<Transform> _stack = new();
    private int CurrentHeight => _stack.Count;

    private void Awake()
    {
        _brickPool = new ObjectPool<Transform>(CreateBrick, GetBrick, ReleaseBrick);
    }

    private void ReleaseBrick(Transform brick)
    {
        brick.gameObject.SetActive(false);
    }

    private void GetBrick(Transform brick)
    {
        brick.gameObject.SetActive(true);
    }

    private Transform CreateBrick()
    {
        Transform brick = Instantiate(brickPrefab, brickRoot, false);
        return brick;
    }

    public void IncrementHeight(int increment)
    {
        int newHeight = CurrentHeight + increment;
        if (newHeight < 0)
        {
            newHeight = 0;
        }
        
        AdjustHeight(newHeight);
    }
    
    public void AdjustHeight(int height)
    {
        if (CurrentHeight == height || height < 0) return;
        
        mesh.localPosition = new Vector3(0,height * brickHeight, 0);

        if (CurrentHeight < height)
        {
            while (CurrentHeight < height)
            {
                var brick = _brickPool.Get();
                _stack.Add(brick);
            }
        }
        else if (CurrentHeight > height)
        {
            while (CurrentHeight > height)
            {
                var brick = _stack[^1];
                _stack.Remove(brick);
                _brickPool.Release(brick);
            }
        }

        foreach (var brick in _stack)
        {
            brick.localPosition = new Vector3(0,_stack.IndexOf(brick) * brickHeight, 0);
        }
    }

    private void OnValidate()
    {
        if (!brickPrefab || !mesh || !Application.isPlaying) return;
        
        AdjustHeight(numberOfBrick);
    }
}
