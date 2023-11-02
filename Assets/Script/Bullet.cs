using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotateRate = 5f;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private Transform sprite;
    
    private Vector3 _target;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = start.position;
        _target = end.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
        sprite.Rotate(new Vector3(0, 0, rotateRate));

        if (Vector3.Distance(transform.position, start.position) < 0.1f)
        {
            _target = end.position;
        }
        else if (Vector3.Distance(transform.position, end.position) < 0.1f)
        {
            _target = start.position;
        }
    }
}
