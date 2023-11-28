using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedMove = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKey(KeyCode.W)){
        //     transform.Translate(Vector3.forward * speedMove * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.A)){
        //     transform.Translate(Vector3.left * speedMove * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.S)){
        //     transform.Translate(Vector3.back * speedMove * Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.D)){
        //     transform.Translate(Vector3.right * speedMove * Time.deltaTime);
        // }
        transform.position += JoystickControl.direct * speedMove * Time.deltaTime;
    }
}
