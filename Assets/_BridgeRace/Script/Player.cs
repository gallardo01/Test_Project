using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private GameObject brickParent;
    GameObject brick;
    [SerializeField] GameObject body;
    [SerializeField] int brickCount = 0;
    public Animator animator;
    private string currentAnim;
    Vector3 nextPoint;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            nextPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            transform.position = check(nextPoint);
            if(JoystickControl.direct != Vector3.zero )
            {
                changeAnim("run");
                body.transform.forward = JoystickControl.direct;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            changeAnim("idle");
        }
        //Debug.DrawRay(transform.position + 0.8f*Vector3.up, transform.down,Color.red);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Brick")
        {
            brick = other.gameObject;
            brick.GetComponent<BoxCollider>().enabled = false;
            Debug.Log(brickCount);
            brick.transform.SetParent(brickParent.transform);
            brick.transform.position = brickParent.transform.position + new Vector3(0, brickCount * 0.2f, 0);
            brick.transform.rotation = body.transform.rotation;
            brickCount++;
        }
    }
    Vector3 check(Vector3 nextPoint)
    {
        RaycastHit hit;
        if(Physics.Raycast(nextPoint + 0.5f*Vector3.up, Vector3.down, out hit,3f, stairLayer))
        {
            Debug.Log(true);
            return hit.point + new Vector3(0,0.9f,0);
        }
        return nextPoint;
    }
    public void changeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
}
