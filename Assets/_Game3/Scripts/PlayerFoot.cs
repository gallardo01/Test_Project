using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    [SerializeField] private Transform brickHolder;
    [SerializeField] private Vector3 nextPosition;
    // Start is called before the first frame update
    void Start()
    {
        // nextPosition = brickHolder.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Brick"){
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.transform.parent.transform.SetParent(brickHolder, false);
            other.gameObject.transform.parent.transform.localPosition = nextPosition;
            nextPosition += new Vector3(0,0.16f,0);
            
        }
    }

}
