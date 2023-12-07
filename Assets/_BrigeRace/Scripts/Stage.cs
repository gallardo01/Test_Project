using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Transform[] brickPoints;
    private List<Vector3> emptyPoints = new List<Vector3>();
    private List<Brick> bricks = new List<Brick>();

    [SerializeField] Brick brickPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        OnInitPoint();
    }

    void OnInitPoint()
    {
        for (int i = 0; i < brickPoints.Length; i++)
        {
            emptyPoints.Add(brickPoints[i].position);
        }
    }

    internal void OnInit(ColorType colorType)
    {
        for (int i = 0; i < 5; i++)
        {
            NewBrick(colorType);
        }
    }

    public void NewBrick(ColorType colorType)
    {
        if (emptyPoints.Count > 0)
        {
            int randomNumber = Random.Range(0, emptyPoints.Count);
            //Brick brick = Instantiate(brickPrefab, emptyPoints[randomNumber], Quaternion.identity);
            Brick brick = EasyObjectPool.instance.GetObjectFromPool("Brick", emptyPoints[randomNumber], Quaternion.identity).GetComponent<Brick>();
            brick.ChangeColor(colorType);
            emptyPoints.RemoveAt(randomNumber);
            bricks.Add(brick);
        }
    }

    public void RemoveBrick(Brick brick)
    {
        StartCoroutine(respawnBrick(brick.colorType));
        emptyPoints.Add(brick.transform.position);
        bricks.Remove(brick);
        EasyObjectPool.instance.ReturnObjectToPool(brick.gameObject);
        brick.gameObject.SetActive(false);
        //Destroy(brick.gameObject);
    }

    IEnumerator respawnBrick(ColorType colorType)
    {
        yield return new WaitForSeconds(3f);
        NewBrick(colorType);
    }
}
