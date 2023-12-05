using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Player : ColorObject
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject brickParent;
    GameObject brick;
    [SerializeField] GameObject body;
    [SerializeField] int brickCount = 0;
    public Animator animator;
    private string currentAnim;
    Vector3 nextPoint;
    void Start()
    {
        ChangeColor(ColorType.Red);
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            nextPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            if(CanMove(nextPoint)) transform.position = check(nextPoint);
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
    }
    IEnumerator spawnBrick()
    {
        yield return new WaitForSeconds(5f);
        Stage.Instance.NewBrick(colorType);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Brick" && other.GetComponent<Brick>().getRenderer().material.name == getRenderer().material.name)
        {
            brick = other.gameObject;
            brick.GetComponent<BoxCollider>().enabled = false;
            brick.transform.SetParent(brickParent.transform);
            brick.transform.position = brickParent.transform.position + new Vector3(0, brickCount * 0.2f, 0);
            brick.transform.rotation = body.transform.rotation;
            brickCount++;
            Stage.Instance.RemoveBrick(brick.GetComponent<Brick>());
            StartCoroutine(spawnBrick());   
        }
    }
    Vector3 check(Vector3 nextPoint)
    {
        RaycastHit hit;
        if(Physics.Raycast(nextPoint + 0.5f*Vector3.up, Vector3.down, out hit,3f, groundLayer))
        {
            return hit.point + new Vector3(0,1f,0);
        }
        return nextPoint;
    }
    private bool CanMove(Vector3 point)
    {
        bool canMove = false;
        if (Physics.Raycast(point, Vector3.down, 2f, groundLayer)) canMove = true;
        return canMove;
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
