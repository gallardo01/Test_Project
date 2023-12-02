using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : Singleton<Stage>
{
    public float x;
    public float z;
    public float distance;
    public Transform[] brickPoints;
    public List<Vector3> emtyPoints = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();
    public Transform BricksBox;

    public Brick BrickPref;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }
    internal void OnInit()
    {
        //for (int i = 0; i < brickPoints.Length; i++)
        //{
        //    emtyPoints.Add(brickPoints[i].position);
        //}
        for(int i = 0; i < 4;i++)
        {           
            for(int j= 0; j< 8; j++)
            {              
                Vector3 newVector = new Vector3(x + distance*j,0,z - distance*i);
                emtyPoints.Add(newVector);
            }
        }
        int count = emtyPoints.Count;
        for(int i = 0; i < count ; i++)
        {
            NewBrick((ColorType)Random.Range(1, 6));
        }
    }

    public void NewBrick(ColorType colorType)
    {
        //if(emtyPoints.Count > 0)
        //{
        //    int randomPoint = Random.Range(0, emtyPoints.Count);
        //    Brick brick = Instantiate(BrickPref, emtyPoints[randomPoint], Quaternion.identity);
        //    brick.changColor(colorType);
        //    emtyPoints.RemoveAt(randomPoint);
        //    bricks.Add(brick);
        //}
        if(emtyPoints.Count > 0)
        {
            int randomPoint = Random.Range(0, emtyPoints.Count);
            Brick brick = Instantiate(BrickPref, emtyPoints[randomPoint], Quaternion.identity);
            brick.changColor(colorType);
            emtyPoints.RemoveAt(randomPoint);
            bricks.Add(brick);
        }
    }
}
