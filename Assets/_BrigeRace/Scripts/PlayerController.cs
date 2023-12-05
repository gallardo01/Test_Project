using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayerController : ColorObject
{
    public float speed = 5f;
    public Animator animator;
    private string currentAnim;
    public Transform body;
    public LayerMask groundLayer;
    public LayerMask stairLayer;

    // Start is called before the first frame update
    void Start()
    {
        ChangeColor(ColorType.Red);
        changeAnim("idle");
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButton(0))
        {
            //transform.position += JoystickControl.direct * speed * Time.deltaTime;

            Vector3 nextPoints = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            if (CanMove(nextPoints))
            {
                transform.position = checkGround(nextPoints);
            }
            // Change anim
            changeAnim("run");
            if (JoystickControl.direct != Vector3.zero)
            {
                body.forward = JoystickControl.direct;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            changeAnim("idle");
        }
    }

    private bool CanMove(Vector3 point)
    {
        bool canMove = false;
        if (Physics.Raycast(point, Vector3.down, 2f, groundLayer))
        {
            canMove = true;
        }
        return canMove;
    }

    private Vector3 checkGround(Vector3 point)
    {
        RaycastHit hit;
        if (Physics.Raycast(point, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 0.3f;
        }
        return point;
    }

    public void changeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }

    }
}
