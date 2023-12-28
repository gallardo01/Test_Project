using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{

    [SerializeField] Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject circleAttack;
    private bool isRun;
    // Start is called before the first frame update
    void Start() 
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            count.Cancel();
            IsWeapon = true;
        }
        if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
        {
            rb.MovePosition(rb.position + JoystickControl.direct * speed * Time.deltaTime);
            transform.position = rb.position;
            transform.forward = JoystickControl.direct;
            ChangAnim(Constants.ANIM_RUN);
        }
        else
        {
            count.Excute();
        }

        //if (Input.GetMouseButtonUp(0))
        //{


        //    if (checkTarget() && IsWeapon )
        //    {
        //        RotateTarget();
        //        ChangAnim(Constants.ANIM_ATTACK);
        //        OnAttack();
        //    }
        //    else
        //    {                
        //        ChangAnim(Constants.ANIM_IDLE);
        //    }
        //}
        if (Input.GetMouseButtonUp(0))
        {
            ChangAnim(Constants.ANIM_IDLE);
        }
        if (!Input.GetMouseButton(0))
        {
            if(checkTarget() && IsWeapon )
            {
                RotateTarget();
                ChangAnim(Constants.ANIM_ATTACK);
                OnAttack();
            }           
        }
    }

    public override void OnAttack()
    {
        //Debug.Log("attack");
        base.OnAttack();
        IsWeapon = false;
        count.Start(ThrowWeapon, 0.4f);
        //ChangAnim(Constants.ANIM_IDLE);
  
    }

}
