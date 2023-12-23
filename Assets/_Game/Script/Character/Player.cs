using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{

    [SerializeField] Rigidbody rb;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start() 
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
        {
            rb.MovePosition(rb.position + JoystickControl.direct * speed * Time.deltaTime);
            transform.position = rb.position;
            transform.forward = JoystickControl.direct;
            ChangAnim(Constants.ANIM_RUN);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Character target = GetTarget();
            if (target != null)
            {
                RotateTarget();
                ChangAnim(Constants.ANIM_ATTACK);

                ThrowWeapon();
            }
            else
            {
                ChangAnim(Constants.ANIM_IDLE);
            }
        }
    }
    public void Attack()
    {
        if (Vector3.Distance(direct, Vector3.zero) >= 0.00001f)
        {
            

        }
  
    }

}
