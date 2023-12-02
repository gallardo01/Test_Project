using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorType{
    Default = 0,
    Black = 1,
    Red = 2,
    Blue,
    Green,
    Yellow,
    Orange,
    Brown,
    Violet
}
public class Stage : Singleton<Stage>
{
    public Transform[] brickPoint;
    public List<Vector3> emptyPoint = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();
    public Transform brickHolder;

    [SerializeField] private Brick brickPrefab;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        InvokeRepeating(nameof(NewBrick), 0f, 1f);
    }

    // void AutoGetEmptyList(){
    //     for(int i = 0; i < bricks.Length; i++){
    //         if(bricks[i].activeInHierarchy == false){
    //             emptyPoint.Add(bricks[i]);
    //             bricks.RemoveAt(i);
    //         }
    //     }
    // }

    public void RemoveBrick(Brick brick){
        emptyPoint.Add(brick.transform.position);
        bricks.Remove(brick);
        // Destroy(brick.gameObject);
    }

    internal void OnInit(){
        for(int i = 0; i < brickPoint.Length; i++){
            emptyPoint.Add(brickPoint[i].position);
        }
    }

    public void InitColor(ColorType colorType){

    }

    public void NewBrick(){
        ColorType colorType = (ColorType)Random.Range(0,9);
        if(emptyPoint.Count > 0){
            int randomNumber = Random.Range(0, emptyPoint.Count);
            Brick brick = Instantiate(brickPrefab, emptyPoint[randomNumber], Quaternion.identity, brickHolder);
            brick.ChangeColor(colorType);
            emptyPoint.RemoveAt(randomNumber);
            bricks.Add(brick);
        }
    }
}
