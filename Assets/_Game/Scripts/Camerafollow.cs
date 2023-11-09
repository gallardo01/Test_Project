using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public float speed = 10;


    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position+offset, speed * Time.deltaTime);
    }
}
