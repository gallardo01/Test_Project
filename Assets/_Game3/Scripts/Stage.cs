using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class Stage : MonoBehaviour
{
    public Transform[] brickPoint;
    public List<Vector3> emptyPoint = new List<Vector3>();
    public List<Brick> brickPoints = new List<Brick>();
    public List<Brick> bricks = new List<Brick>();
    // public Transform brickHolder;

    public List<int> intColorTypeList = new List<int>();
    private List<int> intColorTypeSpawnList = new List<int>();
    [SerializeField] private Brick brickPrefab;
    private bool isOnInit = true;
    private ColorType thisColorType;
    void Awake(){
        // if(isOnInit){
        //     List<int> colorList = new List<int>(){1,2,3,4,5,6,7,8};
        //     for(int i = 0; i < 3; i++){
        //         int colorIndex = Random.Range(0, colorList.Count);
        //         intColorTypeList.Add(colorList[colorIndex]);
        //         intColorTypeSpawnList.Add(colorList[colorIndex]);
        //         colorList.RemoveAt(colorIndex);
        //     }
        //     isOnInit = false;
        // }
    }
    // Start is called before the first frame update
    void Start()
    {
        // OnInit();
        // // InvokeRepeating(nameof(NewBrick), 0f, 1f);
        // for(int i = 0; i <4; i++){
        //     NewBrick();
        //     NewBrick();
        //     NewBrick();
        // }
        // isOnInit = false;
    }

    public void RemoveBrick(Brick brick){
        // emptyPoint.Add(brick.transform.position);
        brickPoints.Add(brick);
        brick.gameObject.SetActive(false);
        bricks.Remove(brick);
        Invoke(nameof(NewBrick), 3f);
        // Destroy(brick.gameObject);
    }

    private void OnInitPoints(){
        for(int i = 0; i < brickPoint.Length; i++){
            emptyPoint.Add(brickPoint[i].position);
        }
    }

    internal void OnInit(ColorType colorType){
        for(int i = 0; i <4; i++){
            NewBrick();
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
        // if(newColorIndex != 0 && !isOnInit){
        //     // Invoke(nameof(NewBrick), 3f);
        //     SpawnNewBrick();
        // }
        ColorType newColor = (ColorType)intColorTypeSpawnList[newColorIndex];
        intColorTypeSpawnList.RemoveAt(newColorIndex);
        return newColor;
    }

    public void SpawnNewBrick(){
        Invoke(nameof(NewBrick), 1f);
        Invoke(nameof(NewBrick), 2f);
    }

    public void NewBrickByColor(){

    }

    public void NewBrick(){
        // ColorType colorType = RandomColor();
        ColorType colorType = LevelManager.Ins.colorPlayer;
        if(brickPoints.Count > 0){
            int randomNumber = Random.Range(0, brickPoints.Count);
            // Debug.Log("Truoc: " + emptyPoint.Count + " " + emptyPoint[randomNumber]);
            // Brick brick = Instantiate(brickPrefab, emptyPoint[randomNumber], Quaternion.identity, brickHolder);
            brickPoints[randomNumber].gameObject.SetActive(true);
            brickPoints[randomNumber].ChangeColor(colorType);
            // emptyPoint.RemoveAt(randomNumber);
            bricks.Add(brickPoints[randomNumber]);
            brickPoints.RemoveAt(randomNumber);
            // Debug.Log("Sau: " + emptyPoint.Count);
        }
    }
}
