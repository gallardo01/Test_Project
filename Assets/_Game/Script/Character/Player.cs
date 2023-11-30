using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {

        Vector3 nextPoint = JoytickController.direct * moveSpeed * Time.deltaTime + transform.position;       
        Debug.DrawRay(nextPoint, Vector3.down, Color.green, lenghtRaycast);

        if (Input.GetMouseButton(0))
        {
            changAnim("run");
            if (UpStair(nextPoint))
            {
                transform.position = checkGround(nextPoint);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            changAnim("idle");
        }


        if (JoytickController.direct != Vector3.zero)
        {
            PlayerSkin.transform.forward = JoytickController.direct;
        }

    }

   

}
