using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Goi y
        transform.position += JoystickControl.direct * speed * Time.deltaTime;
    }

    //public Vector3 checkGround(Vector3 nextpoint)
    //{
        
    //}
}
