using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject body;
    

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    void Moving()
    {
        if (Input.GetMouseButton(0))
        {
            JoyStickControl();
            // body.GetComponent<Rigidbody>().useGravity = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnAttack();
        }
    }

    void JoyStickControl()
    {
        Vector3 nextPoint = player.position + JoystickControl.direct * speed * Time.deltaTime;

        if (CanMove(nextPoint))
        {
            Debug.Log("Acess CanMove");
            ChangeAnim(AnimConstant.runAnim);
            player.position = nextPoint;
        }

        if (JoystickControl.direct != Vector3.zero)
        {
            Debug.Log("Acess Diff");
            player.forward = JoystickControl.direct;
        }  
    }
}
