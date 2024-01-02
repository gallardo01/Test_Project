using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;


public class Bullet : MonoBehaviour
{
    public Vector3 destination;
    public bool activeDestination = false;
    [SerializeField] private float speed = 3f;
    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
    }

    public void OnDeath(){
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }

    public void OnMove(Vector3 point){
        Invoke(nameof(OnDeath), 1f);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Character"){
            OnDeath();
        }
    }

}
