using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorType{
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
    public Transform[] brickPoint;
    public List<Vector3> emptyPoint = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();
    public Transform brickHolder;

    public List<int> intColorTypeList = new List<int>();
    private List<int> intColorTypeSpawnList = new List<int>();
    [SerializeField] private Brick brickPrefab;
    void Awake(){
        List<int> colorList = new List<int>(){1,2,3,4,5,6,7,8};
        for(int i = 0; i < 3; i++){
            int colorIndex = Random.Range(0, colorList.Count);
            intColorTypeList.Add(colorList[colorIndex]);
            intColorTypeSpawnList.Add(colorList[colorIndex]);
            colorList.RemoveAt(colorIndex);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        Debug.Log(nameof(brickHolder));
        // InvokeRepeating(nameof(NewBrick), 0f, 1f);
        for(int i = 0; i < 12; i++){
            NewBrick();
        }
    }

    public void RemoveBrick(Brick brick){
        emptyPoint.Add(brick.transform.position);
        bricks.Remove(brick);
        Invoke(nameof(NewBrick), 3f);
        // Destroy(brick.gameObject);
    }

    internal void OnInit(){
        for(int i = 0; i < brickPoint.Length; i++){
            emptyPoint.Add(brickPoint[i].position);
        }
        
    }

    public void InitColor(ColorType colorType){

    }

    private ColorType RandomColor(){
        if(intColorTypeSpawnList.Count <= 0){
            for(int i = 0; i < 3; i++){
                intColorTypeSpawnList.Add(intColorTypeList[i]);
            }
        }
        int newColorIndex = Random.Range(0,intColorTypeSpawnList.Count);
        if(newColorIndex != 0){
            Invoke(nameof(NewBrick), 3f);
        }
        ColorType newColor = (ColorType)intColorTypeSpawnList[newColorIndex];
        intColorTypeSpawnList.RemoveAt(newColorIndex);
        return newColor;
    }

    public void NewBrick(){
        ColorType colorType = RandomColor();
        if(emptyPoint.Count > 0){
            int randomNumber = Random.Range(0, emptyPoint.Count);
            Brick brick = Instantiate(brickPrefab, emptyPoint[randomNumber], Quaternion.identity, brickHolder);
            brick.ChangeColor(colorType);
            emptyPoint.RemoveAt(randomNumber);
            bricks.Add(brick);
        }
    }
}
