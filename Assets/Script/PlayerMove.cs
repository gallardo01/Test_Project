using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;

    public GameObject start;
    public GameObject goal;
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    private Vector2 CurrentPosition;

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

    }

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

    }

     //private void OnTriggerEnter2D(Collider2D collision)
     //{
     //   if (collision.gameObject.tag == "Goal")
     //   {
     //       gameObject.transform.position = start.transform.position;
     //   }
     //}
}
