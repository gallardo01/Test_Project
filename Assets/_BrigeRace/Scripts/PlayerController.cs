using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        changeAnim("idle");
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButton(0))
        {
            //transform.position += JoystickControl.direct * speed * Time.deltaTime;

            Vector3 nextPoints = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            transform.position = checkGround(nextPoints);

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

    private Vector3 checkGround(Vector3 point)
    {
        RaycastHit hit;
        // Cau thang
        if (Physics.Raycast(point, Vector3.down, out hit, 2f, stairLayer))
        {
            return hit.point + Vector3.up * 1.1f;
        }
        // mat dat bthg
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
