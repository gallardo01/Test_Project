using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKey(KeyCode.W)){
        //     transform.Translate(Vector3.forward * speedMove * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.A)){
        //     transform.Translate(Vector3.left * speedMove * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.S)){
        //     transform.Translate(Vector3.back * speedMove * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.D)){
        //     transform.Translate(Vector3.right * speedMove * Time.deltaTime);
        // }
        
        if(Input.GetMouseButton(0)){
            Vector3 nextPositon = transform.position + JoystickControl.direct * speedMove * Time.deltaTime;  
            transform.position = checkGround(nextPositon);

            changeAnim("run");
            if(JoystickControl.direct != Vector3.zero){
                body.forward = JoystickControl.direct;
            }
        }
        if(Input.GetMouseButtonUp(0)){
            changeAnim("idle");
        }
        // Vector3 newPositon = transform.position + JoystickControl.direct * speedMove * Time.deltaTime;  
        // RaycastHit hit;
        // if(Physics.Raycast(center.position, newPositon - center.position, out hit, Mathf.Infinity, groundLayer)){
        //     // playerAnim.SetTrigger("run");
        //     if(hit.collider.gameObject.transform.position.y <= transform.position.y){
        //         transform.position = newPositon;
        //     }
        //     else{
        //         transform.position = hit.collider.gameObject.transform.position + new Vector3(0,0.5f,0);
        //     }
        // }
        // else{
        //     // playerAnim.SetTrigger("idle");
        // }
        
        // else if(Physics.Raycast(center.position, newPositon - center.position, out hit, Mathf.Infinity, stairLayer)){
        //     // transform.position += new Vector3(0,hit.collider.gameObject.transform.position.y - transform.position.y + 0.5f,0);
        //     transform.position = hit.collider.gameObject.transform.position + new Vector3(0,0.5f,0);
        // }
    }

    private Vector3 checkGround(Vector3 point){
        RaycastHit hit;
        if(Physics.Raycast(point, Vector3.down, out hit, 2f, stairLayer)){
            return hit.point + Vector3.up * 1f;
        }
        else if(Physics.Raycast(point, Vector3.down, out hit, 2f, groundLayer)){
            return hit.point - Vector3.up * 1f;
        }
        return point;   
    }

    public void changeAnim(string animName){
        if(currentAnim != animName){
            playerAnim.ResetTrigger(currentAnim);
            currentAnim = animName;
            playerAnim.SetTrigger(currentAnim);
        }
    }

}
