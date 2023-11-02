using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;

    public float x;
    public float y;
    public GameObject start;
    public GameObject goal;
    public GameObject[] points;
    static Vector3 curposition;
    int curpoint;
    Vector2 rantarget;
    

    private float AbsNum(float x, float y)
    {
        if (x - y < 0)
        {
            return y - x;
        }
        return x - y;
    }

    // Start is called before the first frame update
    void Start()
    {
        check_point();
    }

    void check_point()
    {
        curposition = points[curpoint].transform.position;
        rantarget = target();
    }

    private Vector2 target()
    {
        x = Random.Range(0, -2.0f);
        y = Random.Range(0, -2.0f);
        return new Vector2(x, y);
    }

    //private IEnumerator Delay(Vector3 curposition)
    //{
    //    yield return new WaitForSeconds(2.0f);
    //    Movement(curposition);
    //}
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey("w"))
        //{
        //    transform.position += new Vector3(0, 0.1f * Time.deltaTime * speed, 0);
        //}
        //if (Input.GetKey("d"))
        //{
        //    transform.position += new Vector3(0.1f * Time.deltaTime * speed, 0, 0);
        //}
        //if (Input.GetKey("s"))
        //{
        //    transform.position += new Vector3(0, -0.1f * Time.deltaTime * speed, 0);
        //}
        //if (Input.GetKey("a"))
        //{
        //    transform.position += new Vector3(-0.1f * Time.deltaTime * speed, 0, 0);
        //}


        //if (AbsNum(gameObject.transform.position.x, goal.transform.position.x) < 0.1f  && AbsNum(gameObject.transform.position.y, goal.transform.position.y) < 0.1f)
        //{
        //    Debug.Log("Goal");
        //    gameObject.transform.position = start.transform.position;
        //}

        //if (AbsNum(gameObject.transform.position.x, rantarget.x) > 0.5f && AbsNum(gameObject.transform.position.y, rantarget.y) > 0.5f)
        //{
        //    transform.position = Vector3.Lerp(transform.position, rantarget, speed * Time.deltaTime);
        //}
        //else
        //{
        
        
        transform.position = Vector3.MoveTowards(transform.position, curposition, speed * Time.deltaTime);
        
        if (transform.position == curposition)
        {
            curpoint++;
            check_point();
            if (curpoint == points.Length)
            {
                transform.position = start.transform.position;
                curpoint = 0;
            }   
        }
        

        //}
    }     
    
    void Stop()
    {
        speed = 0;
    }
}

     //private void OnTriggerEnter2D(Collider2D collision)
     //{
     //   if (collision.gameObject.tag == "Goal")
     //   {
     //       gameObject.transform.position = start.transform.position;
     //   }
     //}


