using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // [SerializeField] private Transform player;
    // [SerializeField] private GameObject body;
    [SerializeField] private Rigidbody rb;
    [SerializeField] protected float speed = 5f;
    
    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    void Moving()
    {
        if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero && canMove)
        {
            rb.MovePosition(rb.position + JoystickControl.direct * speed * Time.deltaTime);
            transform.position = rb.position;
            transform.forward = JoystickControl.direct;
            changeAnim("run");
        }

        if (Input.GetMouseButtonUp(0) && canMove)
        {
            changeAnim("idle");
                
        }
    }

    // void MoveByJoystick()
    // {
    //     Vector3 nextPoint = player.position + JoystickControl.direct * speed * Time.deltaTime;

    //     if (CanMove(nextPoint))
    //     {
    //         changeAnim(runAnim);
    //         player.position = nextPoint;
    //     }

    //     if (JoystickControl.direct != Vector3.zero)
    //     {
    //         player.forward = JoystickControl.direct;
    //     }  
    // }

    // public void Attack(Vector3 des){
    //     // OnAttack(hand.position, des);
    // }

    // void OnTriggerEnter(Collider other){
    //     if(other.tag == "Bullet"){
    //         Debug.Log("Hit");
    //         OnDeath();
    //     }
    // }


}
