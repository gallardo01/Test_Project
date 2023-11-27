using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private GameObject brick; 
    [SerializeField] private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        brick.GetComponent<Renderer>().enabled = false;
    }

    public void ShowBrick() {
        brick.GetComponent<Renderer>().enabled = true;
        collider.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
