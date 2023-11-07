using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 2;
    public GameObject start;
    public GameObject end;

    private GameObject[] path;
    private GameObject[] path_left;
    private GameObject[] path_right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private GameObject[] returnPath()
    {
        if(Random.Range(0, 2) == 1)
        {

        }
    }
    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, end.transform.position, 0.01f);
       


    }

    private float absNumber(float x, float y)
    {
        if(x - y > 0)
        {
            return x - y;
        }
        return y - x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "End")
        {
            gameObject.transform.position = start.transform.position;
        }
    }
}
