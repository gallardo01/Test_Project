using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += JoystickControl.direct * speed * Time.deltaTime;
    }
    //if(Input.GetKey(KeyCode.W)) {
    //    transform.position += new Vector3(0, 0, speed * Time.deltaTime);
    //}
    //if (Input.GetKey(KeyCode.S))
    //{
    //    transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
    //}
    //if (Input.GetKey(KeyCode.A))
    //{
    //    transform.position += new Vector3(-speed * Time.deltaTime,0,0);
    //}
    //if (Input.GetKey(KeyCode.D))
    //{
    //    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    //}
}
