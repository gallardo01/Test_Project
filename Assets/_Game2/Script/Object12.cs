using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object12 : MonoBehaviour
{
    [SerializeField] private Vector3 worldPosition;
    [SerializeField] private float speed = 0.1f;
    private bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            this.worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.worldPosition.z = 0;
            Vector3 newPos = Vector3.Lerp(transform.position, worldPosition, this.speed);
            transform.position = newPos;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Debug.Log("Complete!");
            canMove = false;
        }
    }

}
