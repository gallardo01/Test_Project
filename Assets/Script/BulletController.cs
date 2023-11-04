using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int speed = 1;
    public  GameObject StartO, EndO, bulletController,target;
    int road = 0;
    // Start is called before the first frame update
    void Start()
    {
        bulletController = GameObject.FindGameObjectWithTag("GameController");
        EndO = bulletController.GetComponent<GameController>().returnEndO();
        StartO = bulletController.GetComponent<GameController>().returnStartO();
        target = EndO;
    }

    // Update is called once per frame
    void Update()
    {
        // if (transform.position == StartO.transform.position)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, EndO.transform.position, 0.01f * speed);
        //     Debug.Log("Go Down");
        // }
        // if (transform.position == EndO.transform.position)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, StartO.transform.position, 0.01f * speed);
        //     Debug.Log("Go Up");
        // }
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.01f * speed);
        if(transform.position == target.transform.position){
            if(road == 0){
                target = StartO;
            }
            else
            {
                target = EndO;
            }
        }
    }
}
