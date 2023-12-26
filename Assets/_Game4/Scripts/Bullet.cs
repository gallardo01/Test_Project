using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;


public class Bullet : MonoBehaviour
{
    public Vector3 destination;
    public bool activeDestination = false;
    // public List<BoxCollider> colliderList = new List<BoxCollider>();
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    void Update()
    {
        if(activeDestination){
            transform.position = Vector3.MoveTowards(transform.position, destination, 0.1f);
            if(transform.position == destination && activeDestination){
                activeDestination = false;
                Invoke(nameof(OnDeath), 1f);
            }
        }
    }

    // public void ActiveCollider(){
    //     for(int i=0; i<colliderList.Count;i++){
    //         if(colliderList[i].gameObject.activeSelf){
    //             colliderList[i].enabled = true;
    //             break;
    //         }
    //     }
    // }

    public void OnDeath(){
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }

    public void SetDestination(Vector3 point){
        activeDestination = true;
        destination = new Vector3(point.x, transform.position.y, point.z) ;
    }

    // void OnTriggerEnter(Collider other){
    //     if(other.tag == "Bot" && other.GetComponent<Player>() != null){
    //         // Debug.Log("Hit Player");
    //         // other.GetComponent<Character>().OnDeath();
    //         Destroy(other.gameObject);
    //     }
    // }

}
