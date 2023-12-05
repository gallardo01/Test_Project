using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ColorObject
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform lowerHalf, brickHolder, body, player;
    [SerializeField] private LayerMask brickLayer, stepLayer, groundLayer;
    [SerializeField] private Animator playerAnim;
    private RaycastHit hit, hitMat;
    [SerializeField] private Material newMaterial;

    private string currentAnim;
    private int brickCount = 0;
    private Vector3 nextPosition, aBrick = new Vector3(0f,0.25f, 0f);
    public List<GameObject> listBrick = new List<GameObject>();
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

        //moveUp();
    }

    // check & add Brick -------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        Material matPlayer = ColorController.Ins.getColorMaterial(colorType);
        if(other.tag == "Brick" && other.gameObject.transform.parent.gameObject.GetComponent<ColorObject>().colorType == colorType)
        {
            Debug.Log("Check Add Brick");

            Stage.Ins.RemoveBrick(other.gameObject.transform.parent.gameObject.GetComponent<Brick>());
            // change brick's position
            other.gameObject.transform.parent.transform.SetParent(brickHolder, false);
            other.gameObject.transform.parent.transform.localPosition = nextPosition;
            nextPosition += aBrick;
            // add brick to list
            listBrick.Add(other.gameObject.transform.parent.gameObject);
            brickCount++;
        }
    }

    // Joystick Controll -------------------------------------------------------------------
    void JoyStickControl()
    {
        Vector3 nextPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
        //transform.position = goingUpStair(nextPoint);
        if (checkGround(nextPoint))
        {
            transform.position = nextPoint;
        }

        if (JoystickControl.direct != Vector3.zero)
        {
            player.forward = JoystickControl.direct;
        }
    }

    // check Step -------------------------------------------------------------------
    bool checkStep()
    {
        return Physics.Raycast(lowerHalf.position, Vector3.down, out hit, 2f, stepLayer);
    }

    // check Ground -------------------------------------------------------------------
    bool checkGround(Vector3 point)
    {
        return Physics.Raycast(point, Vector3.down, out hit, 2f, groundLayer);
    }
    
    // check Material -------------------------------------------------------------------
    bool checkMaterial()
    {
        Material matPlayer = ColorController.Ins.getColorMaterial(colorType);

        Physics.Raycast(lowerHalf.position, Vector3.down, out hitMat, 2f, stepLayer);
        if (hitMat.collider.gameObject.GetComponent<ColorObject>().colorType == colorType)
        {
            return true;
        }
        else return false;
    }

    // Lay a brick down -------------------------------------------------------------------
    void layBrickDown()
    {
        //Material mat3 = Resources.Load<Material>("Mat_3");

        if (checkStep() && checkMaterial() == false)
        {
            if (brickCount > 0)
            {
                Debug.Log("Lay a brick down");
                hit.collider.gameObject.GetComponent<Renderer>().material = ColorController.Ins.getColorMaterial(colorType);
                hit.collider.gameObject.GetComponent<ColorObject>().colorType = colorType;
                Destroy(listBrick[listBrick.Count -1].gameObject);
                listBrick.RemoveAt(listBrick.Count - 1);
                nextPosition -= aBrick;
                brickCount--;
            }
        }
    }
    
    // change anim -------------------------------------------------------------------
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

    void goingUp()
    {
        if (Input.GetMouseButton(0))
        {
            // JoyStickControl();
            Vector3 nextPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
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
    
    private Vector3 goingUpStair(Vector3 point)
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
}
