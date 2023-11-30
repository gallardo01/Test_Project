using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform lowerHalf, brickHolder, body, player;
    [SerializeField] private LayerMask brickLayer, stepLayer;
    [SerializeField] private Animator playerAnim;
    private string currentAnim;
    private int brick = 0;
    // Start is called before the first frame update
    void Start()
    {
        changeAnim("idle");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.DrawLine(lowerHalf.position, lowerHalf.position + new Vector3(0f, 0.25f, 0f), Color.red);
        //Moving();
        if (Input.GetMouseButton(0))
        {
            // JoyStickControl();
            Vector3 nextPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            transform.position = checkGround(nextPoint);

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

        //moveUp();
    }

    private Vector3 checkGround(Vector3 point)
    {
        RaycastHit hit;
        // Cau thang
        if (Physics.Raycast(point, Vector3.down, out hit, 2f, stepLayer))
        {
            return hit.point + Vector3.up * 1.1f;
        }
        // mat dat binh thuong
        return point;
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

    void JoyStickControl()
    {
        transform.position += JoystickControl.direct * speed * Time.deltaTime;
    }

    private bool checkHitStep()
    {
        return Physics.Raycast(lowerHalf.position, new Vector3(0f, 0f, 0.2f), brickLayer);
    }

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
    
    void changeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            playerAnim.ResetTrigger(currentAnim);
            currentAnim = animName;
            playerAnim.SetTrigger(currentAnim);
        }
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
}
