using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public float distance;
    public List<Vector3> emtyPoints = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        if (transform.name == ObjectPooling.Ins.Pools[0].name)
        {
            ActiveAllBrick();
        }
    }
    internal void OnInit()
    {
        Transform parent = transform.parent;
        float x = parent.position.x - 6.5f;
        float y = parent.position.y;
        float z = parent.position.z;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3 newVector = new Vector3(x + distance * j, y, z - distance * i);
                emtyPoints.Add(newVector);
            }
        }
    }



    public void ActiveAllBrick()
    {
        Debug.Log("t1");
        List<GameObject> obj = ObjectPooling.Ins.BrickPools[transform.name];
        foreach (GameObject obj2 in obj)
        {
            obj2.SetActive(true);
            obj2.GetComponent<Brick>().changColor(ObjectPooling.Ins.RandomColorInStage(transform.name));
            obj2.transform.position = ReturnEmtyPoint();
        }
    }
   
    public Vector3 ReturnEmtyPoint()
    {
        int pointRandom = Random.Range(0, emtyPoints.Count);
        Vector3 EmtyPoint = emtyPoints[pointRandom];
        emtyPoints.RemoveAt(pointRandom);
        return EmtyPoint;
    }

    public IEnumerator RespawnBrick()
    {
        yield return new WaitForSeconds(ObjectPooling.Ins.respawnTime);
        if (emtyPoints.Count > 0)
        {
            ObjectPooling.Ins.GetBrickFromPool(transform.name).transform.position = ReturnEmtyPoint();
        }
    }

}
