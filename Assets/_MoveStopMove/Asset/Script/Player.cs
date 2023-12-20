using ChangeAnim;
using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Character
{
    private Transform playerTransform;
    [SerializeField] Rigidbody rb;
    [SerializeField] protected float speed;
    
    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
        OnInit();
    }
    void FixedUpdate()
    {
        
        if(Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
        {
            rb.MovePosition(rb.position + JoystickControl.direct * speed * Time.fixedDeltaTime);
            playerTransform.position = rb.position;
            playerTransform.forward = JoystickControl.direct;
            changeAnim(Constants.ANIM_RUN);
        }
        else
        {
            changeAnim(Constants.ANIM_IDLE);
        }
    }
}
