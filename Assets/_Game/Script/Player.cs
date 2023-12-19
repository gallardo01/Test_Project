using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
        {
            rb.MovePosition(rb.position + JoystickControl.direct * moveSpeed * Time.deltaTime);
            transform.position = rb.position;
            transform.forward = JoystickControl.direct;
            changeAnim("run");
        }

        if (Input.GetMouseButtonUp(0))
        {
            changeAnim("idle");
        }
    }
}
