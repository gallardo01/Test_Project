using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ColorObject
{
    [SerializeField] private float speedMove = 5;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private Transform center;
    [SerializeField] private Transform body;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private PlayerFoot foot;
    public List<PlayerBrick> listBrickHold = new List<PlayerBrick>();
    [SerializeField] private PlayerBrick playerBrickPrefab;
    public Stage stage;
    // private ColorType colorType;

    // [SerializeField] private GameObject leftStair;
    
    private string currentAnim;
    // Start is called before the first frame update
    void Start()
    {
        changeAnim("idle");
        // colorType = ColorType.Default;
        // leftStair.GetComponent<Renderer>().material = ColorController.Ins.getColorMaterial(ColorType.Yellow);
    }

    // Update is called once per frame
    void Update()
    {        
        if(Input.GetMouseButton(0)){
            Vector3 nextPositon = transform.position + JoystickControl.direct * speedMove * Time.deltaTime;  
            if(CanMove(nextPositon)){
                transform.position = checkGround(nextPositon);
            }
            BrickDownStair(transform.position);

            changeAnim("run");
            if(JoystickControl.direct != Vector3.zero){
                body.forward = JoystickControl.direct;
            }
        }
        if(Input.GetMouseButtonUp(0)){
            changeAnim("idle");
        }
    }

    private void BrickDownStair(Vector3 point){
        RaycastHit hit;
        if(Physics.Raycast(point, Vector3.down, out hit, 2f, stairLayer) && hit.collider.gameObject.GetComponent<ColorObject>().colorType != colorType && listBrickHold.Count > 0){
            // return hit.point + Vector3.up * 0.05f;
            hit.collider.gameObject.GetComponent<ColorObject>().ChangeColor(colorType);
            Destroy(listBrickHold[listBrickHold.Count - 1].gameObject);
            listBrickHold.RemoveAt(listBrickHold.Count - 1);
            foot.ReduceNextPosition();
        }
        // return 
    }



    private Vector3 checkGround(Vector3 point){
        RaycastHit hit;
        if(Physics.Raycast(point, Vector3.down, out hit, 2f, groundLayer)){
            return hit.point + Vector3.up * 0.05f;
        }
        // else if(Physics.Raycast(point, Vector3.down, out hit, 2f, groundLayer)){
        //     return hit.point - Vector3.up * 1f;
        // }
        return point;
    }

    private bool CanMove(Vector3 point){
        bool canMove = false;
        if(Physics.Raycast(point, Vector3.down, 2f, groundLayer)){
            RaycastHit hit;
            Debug.Log(Physics.Raycast(point, Vector3.down, 2f, stairLayer));
            if(Physics.Raycast(point, Vector3.down, out hit, 2f, stairLayer)){
                if(hit.collider.gameObject.GetComponent<ColorObject>().colorType == colorType || (listBrickHold.Count > 0)){
                    canMove = true;
                }
            }
            else{
                canMove = true;
            }
            
        }
        return canMove;
    }

    public void ProcessBrick(Vector3 nextPosition, Transform brickHolder){
        PlayerBrick brickSpawned = Instantiate(playerBrickPrefab, brickHolder.position + nextPosition, brickHolder.rotation, brickHolder);
        // brickSpawned.gameObject.transform.rotation = ;
        listBrickHold.Add(brickSpawned);
        brickSpawned.ChangeColor(colorType);
    }

    public void changeAnim(string animName){
        if(currentAnim != animName){
            if(currentAnim != ""){
                playerAnim.ResetTrigger(currentAnim);
            }
            currentAnim = animName;
            playerAnim.SetTrigger(currentAnim);
        }
    }

}
