using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public bool enterRange = false, onTarget = false;
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {   
        if (other.tag == Tag.botTag)
        {
            Debug.Log("Target = True");
            onTarget = true;
            target = other.gameObject.transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == Tag.botTag)
        {
            onTarget = false;
        }
    }
}
