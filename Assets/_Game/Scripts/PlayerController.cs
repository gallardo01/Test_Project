using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            // transform.position += Vector3.forward * Time.deltaTime * speed;
            direct = Direct.Forward;
        }
        if (Input.GetKey("a"))
        {
            direct = Direct.Left;
        }
        if (Input.GetKey("s"))
        {
            direct = Direct.Back;
        }
        if (Input.GetKey("d"))
        {
            direct = Direct.Right;
        }

        //-----------------------------------------------------------------------
        if (direct == Direct.None)
        {
            transform.Translate(Vector3.zero * Time.deltaTime * speed);
        }
        else if (direct == Direct.Back)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        else if (direct == Direct.Right)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if (direct == Direct.Left)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (direct == Direct.Forward)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

public enum Direct
    {
        None,
        Back,
        Right,
        Left,
        Forward
    }
    
    public Direct direct;
}
