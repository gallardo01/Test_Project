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
    //[SerializeField] private Material newMaterial;

    private string currentAnim;
    private int brickCount = 0;
    private Vector3 nextPosition, aBrick = new Vector3(0f,0.25f, 0f);
    public List<GameObject> listBrick = new List<GameObject>();
    [SerializeField] private PlayerBrick playerBrickPrefab;
    public Stage stage;
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

    // check & add Brick -------------------------------------------------------------------
    void OnTriggerStay(Collider other)
    {
        //Material matPlayer = ColorController.Ins.getColorMaterial(colorType);
        // other.tag == "Brick" && other.gameObject.transform.parent.gameObject.GetComponent<ColorObject>().colorType == colorType
        // if(other.tag == "Brick" && other.gameObject.transform.parent.gameObject.GetComponent<ColorObject>().colorType == colorType)
        if(checkMaterial(brickLayer))
        {
            stage.RemoveBrick(other.gameObject.transform.parent.gameObject.GetComponent<Brick>());
            // Debug.Log("Add Brick");
            // change brick's position
            // other.gameObject.transform.parent.transform.SetParent(brickHolder, false);
            // other.gameObject.transform.parent.transform.localPosition = nextPosition;
            // listBrick.Add(other.gameObject.transform.parent.gameObject);

            // Instantiate brick
            PlayerBrick brickGet = Instantiate(playerBrickPrefab, brickHolder.position + nextPosition, player.rotation, brickHolder);
            brickGet.ChangeColor(colorType);
            listBrick.Add(brickGet.gameObject);

            // add brick to list
            nextPosition += aBrick;
            brickCount++;
        }
    }

    // Joystick Controll -------------------------------------------------------------------
    void JoyStickControl()
    {
        Vector3 nextPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
        //Vector3 nextStepPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
        //transform.position = goingUpStair(nextPoint);
        if (checkLayer(nextPoint, groundLayer) && CanMove(nextPoint))
        {
            transform.position = nextPoint;
        }

        if (JoystickControl.direct != Vector3.zero)
        {
            player.forward = JoystickControl.direct;
        }  
    }

    // check layer
    bool checkLayer(Vector3 point, LayerMask layer)
    {
        return Physics.Raycast(point, Vector3.down, out hit, 2f, layer);
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
    
    // check Material -------------------------------------------------------------------
    bool checkMaterial(LayerMask layer)
    {
        // Debug.Log(Physics.Raycast(lowerHalf.position, Vector3.down, out hitMat, 10f, layer));
        //Material matPlayer = ColorController.Ins.getColorMaterial(colorType);
        // Debug.Log(layername);
        Debug.DrawLine(lowerHalf.position, lowerHalf.position + Vector3.down * 100f, Color.red);
        if (Physics.Raycast(lowerHalf.position, Vector3.down, out hitMat, 100f, layer))
        {
            // Debug.Log(hitMat.collider.gameObject.GetComponent<Brick>());
            if (hitMat.collider.gameObject.GetComponent<ColorObject>() != null)
            {
                if (hitMat.collider.gameObject.GetComponent<ColorObject>().colorType == colorType)
                {
                    return true;
                }
            }
            else if (hitMat.collider.gameObject.transform.parent.gameObject.GetComponent<ColorObject>() != null)
            {
                if (hitMat.collider.gameObject.transform.parent.gameObject.GetComponent<ColorObject>().colorType == colorType)
                {
                    return true;
                }

            }
        }
        return false;
    }

    private bool CanMove(Vector3 point)
    {
        bool canMove = false;
        if (Physics.Raycast(point, Vector3.down, 2f, groundLayer))
        {
            RaycastHit hit1;
            // Debug.Log(Physics.Raycast(point, Vector3.down, 2f, stepLayer));
            if (Physics.Raycast(point, Vector3.down, out hit1, 2f, stepLayer))
            {
                if (hit1.collider.gameObject.GetComponent<ColorObject>().colorType == colorType || (brickCount > 0))
                {
                    canMove = true;
                }
            }
            else
            {
                canMove = true;
            }
            
        }
        return canMove;
    }

    // Lay a brick down -------------------------------------------------------------------
    void layBrickDown()
    {
        //Material mat3 = Resources.Load<Material>("Mat_3");
        Vector3 nextPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
        // Debug.Log("Lay Brick " + checkMaterial(stepLayer));
        if (checkLayer(nextPoint, stepLayer) && !checkMaterial(stepLayer))
        {
            if (brickCount > 0)
            {
                Debug.Log("Lay a brick down");
                hit.collider.gameObject.GetComponent<Renderer>().material = ColorController.Ins.getColorMaterial(colorType);
                hit.collider.gameObject.GetComponent<ColorObject>().colorType = colorType;
                Debug.Log(hit.collider.gameObject.transform.position);
                Destroy(listBrick[listBrick.Count -1].gameObject);
                listBrick.RemoveAt(listBrick.Count - 1);
                nextPosition -= aBrick;
                brickCount--;
                hit = new RaycastHit();
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
