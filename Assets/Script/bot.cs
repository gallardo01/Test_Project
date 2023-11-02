using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private GameObject[] path1;
    public GameObject start;
    public GameObject end;
    private int step=0;

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = start.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
               
        transform.position = Vector3.MoveTowards(transform.position, path1[step].transform.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, path1[step].transform.position) < 0.1f)
        {
            step++;
        }

        if (Vector3.Distance(transform.position, path1[4].transform.position) < 0.1f)
        {
            transform.position = start.transform.position;
            step = 0;
        }

    }
}
