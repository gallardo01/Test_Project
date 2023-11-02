
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnmove : MonoBehaviour
{
    public GameObject[] points;
    public float speed;
    int step = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[step].transform.position, speed * Time.deltaTime);
        if (transform.position == points[step].transform.position)
        {
            step++;
            if (step == 0)
            {
                
            }
            if (step == points.Length)
            {
                step = 0;
            }
        }
    }
}
