using System.Collections;
using System.Collections.Generic;
using MarchingBytes;
using UnityEngine;
// public enum ColorType{
//     Default,
//     Black,
//     Blue,
//     Cyan,
//     Green,
//     Pink
// }
public class Stage : MonoBehaviour
{
    public Transform[] brickPoints;
    public List<Vector3> emptyPoints = new List<Vector3>();
    public List<Brick> bricks = new List<Brick>();
    public List<Brick> brickInits = new List<Brick>();
    [SerializeField] Brick brickPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // OnInit();
        // for (int i = 0; i < 5; i++)
        // {
        //     SpawnNewBrick(ColorType.Pink);
        //     SpawnNewBrick(ColorType.Cyan);
        // }
    }

    internal void OnInit(ColorType colorType)
    {
        for (int i = 0; i < brickPoints.Length; i++)
        {
            emptyPoints.Add(brickPoints[i].position);
        }
        for (int i = 0; i < 5; i++)
        {
            SpawnNewBrick(ColorType.Cyan);
        }
    }  
    public void InitColor(ColorType colorType)
    {

    }

    public void SpawnNewBrick (ColorType colorType)
    {
        if (emptyPoints.Count > 0)
        {
            // Debug.Log("InInit");
            int randomNumber = Random.Range(0, emptyPoints.Count);
            // Brick brick = Instantiate (brickPrefab, emptyPoints[randomNumber], Quaternion.identity);
            // brick.ChangeColor(colorType);
            // brickInits[randomNumber].gameObject.SetActive(true);
            Brick brick = EasyObjectPool.instance.GetObjectFromPool("Brick", emptyPoints[randomNumber], Quaternion.identity).GetComponent<Brick>();
            brick.ChangeColor(colorType);
            bricks.Add(brick);
            // brickInits.RemoveAt(randomNumber);
            emptyPoints.RemoveAt(randomNumber);
        }
    }
    

    public void RemoveBrick (Brick brick)
    {
        emptyPoints.Add(brick.transform.position);
        EasyObjectPool.instance.ReturnObjectToPool(brick.gameObject);
        // brickInits.Add(brick);
        // brick.gameObject.SetActive(false);
        bricks.Remove(brick);
        
        // need function later
        // Debug.Log("Remove");
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
        // Debug.Log("respawn");
        yield return new WaitForSeconds(3f);
        SpawnNewBrick(colorType);
    }
}