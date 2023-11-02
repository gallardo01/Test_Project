using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectmovement : MonoBehaviour
{
    [SerializeField] private float speed;

    public GameObject goal;
    static Vector3 curposition;
    int curpoint;
    //public GameObject[] points;
    // Start is called before the first frame update
    void Start()
    {
        //check_point();
    }

    private float AbsNum(float x, float y)
    {
        if (x - y < 0)
        {
            return y - x;
        }
        else
            return x - y;
    }

    //void check_point()
    //{         
    //    curposition = points[curpoint].transform.position;
    //}
    // Update is called once per frame
    void Update()
    {
        //if (AbsNum(gameObject.transform.position.x, curposition.x) > 0.1f && AbsNum(gameObject.transform.position.y, curposition.y) > 0.1f)
        //{
        //    transform.position = Vector3.Lerp(transform.position, curposition, speed * Time.deltaTime);
        //}

        //else
        //{
        //    if (curpoint < points.Length - 1)
        //    {
        //        curpoint++;
        //        check_point();
        //    }
        //}
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = mousePosition;
        if (AbsNum(gameObject.transform.position.x, goal.transform.position.x) < 0.1f && AbsNum(gameObject.transform.position.y, goal.transform.position.y) < 0.1f)
        {
            Debug.Log("success");
        }
    }
}
