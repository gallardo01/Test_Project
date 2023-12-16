using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Transform player, destination;
    private bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            OnShoot();
        }
    }

    public void OnShoot()
    {
        destination.position = transform.position + Vector3.forward * 5f;
        transform.position = Vector3.MoveTowards(transform.position, destination.position, 0.1f);
    }
}
