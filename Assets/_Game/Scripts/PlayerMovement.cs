using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direct{
    Forward,
    Back,
    Left,
    Right,
    None
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform DirectionPoint;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float speed = 1;
    // [SerializeField] private Direct direct;
    private Vector3 direction = Vector3.zero;
    private Vector2 startPos, endPos;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKey("w")){
        //     direct = Direct.Forward;
        // }
        // CheckForwardDirection();
        if(Input.GetMouseButtonDown(0)){
            startPos = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0)){
            endPos = Input.mousePosition;
            ChangeDirection();
            startPos = Vector3.zero;
            endPos = Vector3.zero;
        }
        transform.Translate(direction * speed * Time.deltaTime );
        
    }

    private void ChangeDirection(){
        if(Mathf.Abs(endPos.x - startPos.x) > Mathf.Abs(endPos.y - startPos.y)){
            if(endPos.x - startPos.x < 0){
                // transform.Translate(Vector3.right * speed /** Time.deltaTime*/);
                direction = Vector3.right;
            } 
            else{
                // transform.Translate(Vector3.left * speed /** Time.deltaTime*/);
                direction = Vector3.left;
            }
        }
        else if(Mathf.Abs(endPos.x - startPos.x) < Mathf.Abs(endPos.y - startPos.y)){
            if(endPos.y - startPos.y < 0){
                // transform.Translate(Vector3.forward * speed /** Time.deltaTime*/);
                direction = Vector3.forward;
            } 
            else{
                // transform.Translate(Vector3.back * speed /** Time.deltaTime*/);
                direction = Vector3.back;
            }
        }
    }

    private bool CheckCanMove(){
        RaycastHit2D hit = Physics2D.Raycast(DirectionPoint.position, Vector3.forward, 1.1f, wallLayer);

        return hit.collider != null;
    }

    // private bool CheckForwardDirection(){
    //     Debug.DrawLine(DirectionPoint.position, DirectionPoint.position + Vector3.forward * 1.1f, Color.red);
    //     RaycastHit2D hit = Physics2D.Raycast(DirectionPoint.position, Vector3.forward, 1.1f);

    //     return hit.collider != null;

    // }

    // private bool CheckBackwardDirection(){
    //     return true;
    // }

    // private bool CheckLeftDirection(){
    //     return true;
    // }

    // private bool CheckRightDirection(){
    //     return true;
    // }

}
