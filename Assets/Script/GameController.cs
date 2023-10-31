using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            Debug.Log("A");
        }
        else if (Input.GetKey("b"))
        {
            Debug.Log("B");
        }
        else if (Input.GetKey("c"))
        {
            Debug.Log("C");
        }
    }
}
