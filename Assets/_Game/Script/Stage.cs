using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public float x;
    public float y;
    public float z;
    public float distance;
    public List<Vector3> emtyPoints = new List<Vector3>();
    //public Transform[] brickPoints;
    //public List<Brick> bricks = new List<Brick>();
    //public Transform BricksBox;

    //public Brick BrickPref;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        ActiveAllBrick();
    }
    internal void OnInit()
    {

        for(int i = 0; i < 4;i++)
        {           
            for(int j= 0; j< 8; j++)
            {              
                Vector3 newVector = new Vector3(x + distance*j,y,z - distance*i);
                emtyPoints.Add(newVector);
            }
        }
    }

    private void ActiveAllBrick()
    {
        Debug.Log("t1");
        List<GameObject> obj = ObjectPooling.Ins.BrickPools[transform.name];
        foreach(GameObject obj2 in obj)
        {
            obj2.SetActive(true);
            obj2.transform.position = ReturnEmtyPoint();
        }
    }
    //public void NewBrick(ColorType colorType)
    //{       
    //    if(emtyPoints.Count > 0)
    //    {
    //        int randomPoint = Random.Range(0, emtyPoints.Count);
    //        Brick brick = Instantiate(BrickPref, emtyPoints[randomPoint], Quaternion.identity);
    //        brick.changColor(colorType);
    //        emtyPoints.RemoveAt(randomPoint);
    //        bricks.Add(brick);
    //    }
    //}
    public Vector3 ReturnEmtyPoint()
    {
        int pointRandom = Random.Range(0, emtyPoints.Count);
        Vector3 EmtyPoint = emtyPoints[pointRandom];
        emtyPoints.RemoveAt(pointRandom);
        return EmtyPoint;
    }
    //public void spawn()
    //{
    //    if(ObjectPooling.Ins.GetBrickFromPool(transform.name) == null)
    //    {
    //        return;
    //    }
    //    ObjectPooling.Ins.GetBrickFromPool(transform.name).transform.position = ReturnEmtyPoint();
    //}
    public IEnumerator RespawnBrick()
    {
        yield return new WaitForSeconds(ObjectPooling.Ins.respawnTime);
        if (emtyPoints.Count > 0)
        {
            ObjectPooling.Ins.GetBrickFromPool(transform.name).transform.position = ReturnEmtyPoint();
        }
    }

}
