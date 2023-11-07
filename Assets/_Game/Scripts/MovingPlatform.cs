using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform aPoint, bPoint;
    [SerializeField] private float speed;
    private Vector3 target;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = aPoint.position;    
        target = bPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    
        if(Vector2.Distance(transform.position, aPoint.position) < 0.1f){
            target = bPoint.position;
        }
        else if(Vector2.Distance(transform.position, bPoint.position) < 0.1f){
            target = aPoint.position;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player"){
            other.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player"){
            other.transform.SetParent(null);
        }
    }

}
