using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using MarchingBytes;

public class Bullet : MonoBehaviour
{
    public Vector3 destination;
    private bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position + transform.forward * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        CanMove();

        if (canMove)
        {
            Debug.Log("canMove = true");
            OnShoot();
            if (transform.position == destination)
            {
                canMove = false;
                EasyObjectPool.instance.ReturnObjectToPool(gameObject);
            }
        }
    }

    void CanMove()
    {
        canMove = true;
    }
    public void OnShoot()
    {   
        Debug.Log("OnShoot");
        transform.position = Vector3.MoveTowards(transform.position, destination, 0.02f);
    }

    public void SetDestination(Vector3 target)
    {
        destination = target;
    }
}
