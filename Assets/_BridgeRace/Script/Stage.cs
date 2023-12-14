using MarchingBytes;
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
public class Stage : MonoBehaviour
{
    public Transform[] brickPoints;
    private List<Vector3> emptyPoints = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();
    [SerializeField] Brick brickPrefab;
    private void Start()
    {
        OnInitPoint();
    }
    void OnInitPoint()
    {
        for (int i = 0; i < brickPoints.Length; i++)
        {
            emptyPoints.Add(brickPoints[i].transform.position);
        }
    }
    internal void OnInit(ColorType colorType)
    {
        
        for(int i = 0;i<5;i++)
        {
            NewBrick(colorType);
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
            Brick brick = EasyObjectPool.instance.GetObjectFromPool("Brick", emptyPoints[randomNumber], Quaternion.identity).GetComponent<Brick>();
            brick.ChangeColor(colorType);
            emptyPoints.RemoveAt(randomNumber);
            bricks.Add(brick);
        }
    }
    public void RemoveBrick(Brick brick)
    {
        int i = bricks.IndexOf(brick);
        emptyPoints.Add(brick.gameObject.transform.position);
        bricks.RemoveAt(i);
    }
    internal Brick SeekBrickPoint(ColorType colorType)
    {
        Brick brick = null;
        for(int i=0;i<bricks.Count;i++)
        {
            if (bricks[i].colorType == colorType)
            {
                brick = bricks[i];
                break;
            }
        }
        return brick;
    }
}
