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
    
    private string currentAnim;
    // Start is called before the first frame update
    void Start()
    {
        changeAnim("idle");
        ChangeColor(ColorType.Black);
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
        if(Physics.Raycast(point, Vector3.down, out hit, 2f, stairLayer)){
            // return hit.point + Vector3.up * 0.05f;
            hit.collider.gameObject.GetComponent<ColorObject>().ChangeColor(colorType);
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
            canMove = true;
        }
        return canMove;
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
