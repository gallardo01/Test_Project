using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    // Start is called before the first frame update
    public int speed = 1;
    public GameObject start;
    public GameObject end;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, end.transform.position, 0.01f);

        //if (Input.GetKey("w"))
        //{
        //    transform.Translate(Vector3.up * Time.deltaTime * speed);
        //}
        //else if (Input.GetKey("a")) 
        //{
        //    transform.Translate(Vector3.left * Time.deltaTime * speed);
        //}
        //else if (Input.GetKey("s"))
        //{
        //    transform.Translate(Vector3.down * Time.deltaTime * speed);
        //}
        //else if (Input.GetKey("d"))
        //{
        //    transform.Translate(Vector3.right * Time.deltaTime * speed);
        //}

        //if( absNumber(gameObject.transform.position.x , end.transform.position.x)< 1f
        //    && absNumber(gameObject.transform.position.y , end.transform.position.y)<1f)
        //{
        //    //gan vi tri
        //    gameObject.transform.position = start.transform.position;

        //}

    }
    private float absNumber(float x, float y)
    {
        if (x - y > 0)
        {
            return x - y;
        }
        return y - x;
    }
    //
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "End")
        {

            gameObject.transform.position = start.transform.position;

        }
    }
}
