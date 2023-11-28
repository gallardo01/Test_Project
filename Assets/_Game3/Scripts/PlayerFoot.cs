using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    [SerializeField] private Transform brickHolder;
    [SerializeField] private Vector3 nextPos;
    // Start is called before the first frame update
    void Start()
    {
        // nextPos = brickHolder.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Brick"){
            other.gameObject.transform.parent.transform.SetParent(brickHolder);
            other.gameObject.transform.parent.transform.position = Vector3.zero;
            nextPos += new Vector3(0,0.15f,0);
        }
    }

}
