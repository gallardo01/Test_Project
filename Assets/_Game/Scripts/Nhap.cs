using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nhap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // isGrounded = CheckGrounded();

        // if (Input.GetKey("w"))
        // {
        //     // transform.position += Vector3.forward * Time.deltaTime * speed;
        //     direct = Direct.Forward;
        // }
        // if (Input.GetKey("a"))
        // {
        //     direct = Direct.Left;
        // }
        // if (Input.GetKey("s"))
        // {
        //     direct = Direct.Back;
        // }
        // if (Input.GetKey("d"))
        // {
        //     direct = Direct.Right;
        // }

        // //-----------------------------------------------------------------------
        // if (direct == Direct.None)
        // {
        //     transform.Translate(Vector3.zero * Time.deltaTime * speed);
        // }
        // else if (direct == Direct.Back)
        // {
        //     transform.Translate(Vector3.back * Time.deltaTime * speed);
        // }
        // else if (direct == Direct.Right)
        // {
        //     transform.Translate(Vector3.right * Time.deltaTime * speed);
        // }
        // else if (direct == Direct.Left)
        // {
        //     transform.Translate(Vector3.left * Time.deltaTime * speed);
        // }
        // else if (direct == Direct.Forward)
        // {
        //     transform.Translate(Vector3.forward * Time.deltaTime * speed);
        // }
    }

    // public enum Direct
//     {
//         None,
//         Back,
//         Right,
//         Left,
//         Forward
//     }
//     public Direct direct;

//     private bool CheckGrounded(){
//         // Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);
//         RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

//         return hit.collider != null;
//     }

}
