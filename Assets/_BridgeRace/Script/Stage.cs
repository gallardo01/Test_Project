using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
public enum ColorType
{
    Default,
    Black,
    Red,
    Green,
    Yellow,
    Orange,
    Brown,
    Violet
}
public class Stage : Singleton1<Stage>
{
    public Transform[] brickPoints;
    private List<Vector3> emptyPoints = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();
    [SerializeField] Brick brickPrefab;
    private void Start()
    {
        OnInit();
        for (int i = 0; i <=7; i++)
        {
           NewBrick(ColorType.Green);
           NewBrick(ColorType.Red);
           NewBrick(ColorType.Yellow);
           NewBrick(ColorType.Violet);
           NewBrick(ColorType.Brown);
        }
    }
    internal void OnInit()
    {
        for(int i=0;i<brickPoints.Length;i++)
        {
            emptyPoints.Add(brickPoints[i].transform.position);
        }
    }
    public void InitColor(ColorType colorType)
    {

    }
    public void NewBrick(ColorType colorType)
    {
        if(emptyPoints.Count>0)
        {
            int randomNumber = Random.Range(0, emptyPoints.Count);
           // Debug.Log(emptyPoints[randomNumber]);
            Brick brick = Instantiate(brickPrefab, emptyPoints[randomNumber],Quaternion.identity);
            brick.ChangeColor(colorType);
            emptyPoints.RemoveAt(randomNumber);
            bricks.Add(brick);
        }
    }
    public void RemoveBrick(Brick brick)
    {
        int i = bricks.IndexOf(brick);
        Debug.Log(bricks[i].transform.position);
        Debug.Log(brick.gameObject.transform.position);
        emptyPoints.Add(brick.gameObject.transform.position);
        bricks.RemoveAt(i);
    }
}
