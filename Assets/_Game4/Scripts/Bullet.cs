using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;


public class Bullet : MonoBehaviour
{
    public Vector3 destination;
    public bool activeDestination = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeDestination){
            transform.position = Vector3.MoveTowards(transform.position, destination, 0.05f);
            if(transform.position == destination && activeDestination){
                activeDestination = false;
                Invoke(nameof(OnDeath), 1f);
            }
        }
    }

    public void OnDeath(){
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }

    public void SetDestination(Vector3 point){
        activeDestination = true;
        destination = new Vector3(point.x, transform.position.y, point.z);
    }

}
