using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    // Start is called before the first frame update
    void Start()
    {
        changeAnim("idle");
        ChangeColor(ColorType.Cyan);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.DrawLine(lowerHalf.position, lowerHalf.position + new Vector3(0f, 0.25f, 0f), Color.red);
        layBrickDown();

        if (Input.GetMouseButton(0))
        {
            JoyStickControl();
            // change anim
            changeAnim("run");
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            changeAnim("idle");
            //unable gravity
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        // Debug.Log(Physics.Raycast(lowerHalf.position, Vector3.down, out hitMat, 10f, brickLayer));

        //moveUp();
    }

    // check Step -------------------------------------------------------------------
    bool checkStep(Vector3 point)
    {
        return Physics.Raycast(point, Vector3.down, out hit, 2f, stepLayer);
    }

    // check Ground -------------------------------------------------------------------
    bool checkGround(Vector3 point)
    {
        return Physics.Raycast(point, Vector3.down, out hit, 2f, groundLayer);
    }
    

    // Lan dau tien khi va cham
    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.tag == "End")
    //     {
    //         gameObject.transform.position = start.transform.position;
    //     }
    // }

    // // Update - co dieu kien - 2 vat va cham
    // void OnTriggerStay2D(Collider2D col)
    // {

    // }

    // // 1 lan khi ma k va cham
    // void OnTriggerExit2D(Collider2D col)
    // {

    // }
    void moveUp()
    {
        if (Physics.Raycast(lowerHalf.position, new Vector3(0f, 0f, 0.2f), stepLayer))
        {
            player.position += new Vector3(0f, 0.25f, 0f);
            body.localPosition += new Vector3(0f, 0.25f, 0f);
            lowerHalf.localPosition += new Vector3(0f, 0.25f, 0f);
            brickHolder.localPosition += new Vector3(0f, 0.25f, 0f);
        }
    }
    
    //khong dung nua
    void Moving()
    {
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(-Vector3.forward * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
    }

    private bool checkHitBrick()
    {
        return Physics.Raycast(lowerHalf.position, new Vector3(0f, 0f, 0.2f), brickLayer);
    }

    void goingUp(Vector3 nextPoint)
    {
        if (Input.GetMouseButton(0))
        {
            // JoyStickControl();
            nextPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            transform.position = goingUpStair(nextPoint);

            // change anim
            changeAnim("run");
            if (JoystickControl.direct != Vector3.zero)
            {
                player.forward = JoystickControl.direct;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            changeAnim("idle");
        }
    }
    
}
