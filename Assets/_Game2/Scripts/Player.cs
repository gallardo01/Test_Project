using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Moving();
    }

    //khong dung nua
    void Moving()
    {
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(-Vector3.forward * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
    }

    void JoyStickControl()
    {
        transform.position += JoystickControl.direct * speed * Time.deltaTime;
    }
}
