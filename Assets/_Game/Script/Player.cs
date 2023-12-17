using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] FloatingJoystick joystick;
    [SerializeField] protected LayerMask GroundLayer;
    [SerializeField] Transform playerSkin;
    [SerializeField] float speed;
    private Vector3 direct;
    private float lenghtRaycast = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direct = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        Debug.Log(direct);


        if (Vector3.Distance(direct, Vector3.zero) >= 0.00001f)
        {
            transform.position += direct * speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direct);
        }

        //Vector3 nextPoint = JoystickControl.direct * speed * Time.deltaTime + transform.position;
        //Debug.DrawRay(nextPoint, Vector3.down, Color.green, lenghtRaycast);

        //if (Input.GetMouseButton(0))
        //{
        //    transform.position = checkGround(nextPoint);
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
            
        //}


        //if (JoystickControl.direct != Vector3.zero)
        //{
        //    playerSkin.transform.forward = JoystickControl.direct;
        //}
    }
    public Vector3 checkGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, lenghtRaycast, GroundLayer))
        {
            //Debug.Log(hit.point + Vector3.up);
            return hit.point + Vector3.up;
        }

        return transform.position;
    }
}
