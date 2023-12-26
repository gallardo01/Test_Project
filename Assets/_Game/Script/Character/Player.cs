using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{

    [SerializeField] Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject circleAttack;
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

        if (Input.GetMouseButtonUp(0))
        {
            Character target = GetTarget();
            if (target != null)
            {
                RotateTarget();
                ChangAnim(Constants.ANIM_ATTACK);
                OnAttack();
            }
            else
            {
                ChangAnim(Constants.ANIM_IDLE);
            }
        }
    }
    public override void OnAttack()
    {
        base.OnAttack();
        count.Start(ThrowWeapon, 0.4f);
        //ChangAnim(Constants.ANIM_IDLE);
  
    }

}
