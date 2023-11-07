using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class MovingBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform Star,End;

    Vector3 target; 
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Star.position;
        target = End.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,target,speed * Time.deltaTime);

        if ( Vector2.Distance(transform.position, Star.position) < 0.1f)
        {
            target = End.position; 
        } 
        else if ( Vector2.Distance (transform.position,End.position) < 0.1f ) 
        {
            target=Star.position;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Start")
        {
            gameObject.transform.position = End.transform.position;
        }
    }
}
