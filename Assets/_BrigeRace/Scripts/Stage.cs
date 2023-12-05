using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    Default,
    Black,
    Red,
    Blue,
    Green,
    Yellow,
    Orange,
    Brown,
    Violet
}
public class Stage : Singleton<Stage>
{
    public Transform[] brickPoints;
    private List<Vector3> emptyPoints = new List<Vector3>();
    private List<Brick> bricks = new List<Brick>();

    [SerializeField] Brick brickPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        for (int i = 0; i < 5; i++)
        {
            NewBrick(ColorType.Red);
            NewBrick(ColorType.Green);
        }
    }

    internal void OnInit()
    {
        for (int i = 0; i < brickPoints.Length; i++)
        {
            emptyPoints.Add(brickPoints[i].position);
        }
    }

    public void InitColor(ColorType colorType)
    {

    }

    public void NewBrick(ColorType colorType)
    {
        if (emptyPoints.Count > 0)
        {
            int randomNumber = Random.Range(0, emptyPoints.Count);
            Brick brick = Instantiate(brickPrefab, emptyPoints[randomNumber], Quaternion.identity);
            brick.ChangeColor(colorType);
            emptyPoints.RemoveAt(randomNumber);
            bricks.Add(brick);
        }
    }

    public void RemoveBrick(Brick brick)
    {
        emptyPoints.Add(brick.transform.position);
        bricks.Remove(brick);
        Destroy(brick.gameObject);
        // need function later  
        StartCoroutine(respawnBrick(ColorType.Red));
    }

    IEnumerator respawnBrick(ColorType colorType)
    {
        yield return new WaitForSeconds(3f);
        NewBrick(colorType);
    }
}
