using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot : MonoBehaviour
{

    [SerializeField] private float speedBot;
    //[SerializeField] private GameObject[] path1;
    //[SerializeField] private GameObject[] path2;

    public GameObject[] path1;
    public GameObject[] path2;
    //public GameObject end;
    private int step=0;
    private int x;
    

    // Start is called before the first frame update
    void Start()
    {       
         x = Random.Range(1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (x % 2 == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, path1[step].transform.position, speedBot * Time.deltaTime);
            if (Vector3.Distance(transform.position, path1[step].transform.position) < 0.1f)
            {
                step++;
            }

            if (Vector3.Distance(transform.position, path1[4].transform.position) < 0.1f)
            {
                Score.instance.increaseScore();
                Score.instance.setScore();
                Destroy(gameObject);             
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, path2[step].transform.position, speedBot * Time.deltaTime);
            if (Vector3.Distance(transform.position, path2[step].transform.position) < 0.1f)
            {
                step++;
            }

            if (Vector3.Distance(transform.position, path2[4].transform.position) < 0.1f)
            {
                Score.instance.increaseScore();
                Score.instance.setScore();
                Destroy(gameObject);

            }
        }

    }
}
