using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform aPoint,bPoint;
    [SerializeField] private float speed;
    private Vector3 target;
    void Start()
    {
        transform.position = aPoint.position;
        target = bPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform .position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime);
        if(Vector2.Distance(transform.position,aPoint.position)<0.1f)
        {
            target = bPoint.position;
        }
        else if(Vector2.Distance(transform.position,bPoint.position)<0.1f)
        {
            target = aPoint.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
