using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ColorType{
    Default,
    Black,
    Blue,
    Cyan,
    Green,
    Pink
}
public class Stage : MonoBehaviour
{
    public Transform[] brickPoints;
    public List<Vector3> emptyPoints = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();
    [SerializeField] Brick brickPrefab;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        SpawnNewBrick(ColorType.Cyan);
        SpawnNewBrick(ColorType.Cyan);
        SpawnNewBrick(ColorType.Cyan);
        SpawnNewBrick(ColorType.Cyan);
        SpawnNewBrick(ColorType.Cyan);
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

    public void SpawnNewBrick (ColorType colorType)
    {
        if (emptyPoints.Count > 0)
        {
            int randomNumber = Random. Range(0, emptyPoints.Count);
            Brick brick = Instantiate (brickPrefab, emptyPoints [randomNumber], Quaternion.identity);
            brick.ChangeColor(colorType);
            emptyPoints.RemoveAt(randomNumber);
            bricks.Add(brick);
        }
    }
    

    public void RemoveBrick (Brick brick)
    {
        emptyPoints.Add(brick.transform.position);
        bricks.Remove(brick);
        Destroy(brick.gameObject);
    }
}