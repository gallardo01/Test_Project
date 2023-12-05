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
public class Stage : Singleton<Stage>
{
    public Transform[] brickPoints;
    public List<Vector3> emptyPoints = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();
    [SerializeField] Brick brickPrefab;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        for (int i = 0; i < 5; i++)
        {
        SpawnNewBrick(ColorType.Pink);
        SpawnNewBrick(ColorType.Cyan);
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

    public void SpawnNewBrick (ColorType colorType)
    {
        if (emptyPoints.Count > 0)
        {
            int randomNumber = Random. Range(0, emptyPoints.Count);
            Brick brick = Instantiate (brickPrefab, emptyPoints[randomNumber], Quaternion.identity);
            brick.ChangeColor(colorType);
            emptyPoints.RemoveAt(randomNumber);
            bricks.Add(brick);
        }
    }
    

    public void RemoveBrick (Brick brick)
    {
        emptyPoints.Add(brick.transform.position);
        bricks.Remove(brick);
        
        // need function later
        Debug.Log("Remove");
        //StartCoroutine(respawnBrick(ColorType.Pink));
        if (Random.Range(0, 2) == 0)
        {
            StartCoroutine(respawnBrick(ColorType.Pink));
        }
        else
        {
            StartCoroutine(respawnBrick(ColorType.Cyan));
        }
    }

    IEnumerator respawnBrick(ColorType colorType)
    {
        Debug.Log("respawn");
        yield return new WaitForSeconds(3f);
        SpawnNewBrick(colorType);
    }
}