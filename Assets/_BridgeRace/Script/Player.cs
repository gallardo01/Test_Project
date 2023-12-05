using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Player : ColorObject
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject brickParent;
    GameObject brick;
    [SerializeField] GameObject body;
    List<GameObject> brickObject = new List<GameObject>();
    [SerializeField] int brickCount = 0;
    public Animator animator;
    private string currentAnim;
    Vector3 nextPoint;
    void Start()
    {
        ChangeColor(ColorType.Green);
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
        if (other.tag=="Brick" )
        {
            if (other.GetComponent<Brick>().colorType == colorType || other.GetComponent<Brick>().colorType == ColorType.Brown)
            {
                brick = other.gameObject;
                Stage.Instance.RemoveBrick(brick.GetComponent<Brick>());
                brick.GetComponent<BoxCollider>().enabled = false;
                brick.GetComponent<Brick>().ChangeColor(colorType);
                brick.transform.SetParent(brickParent.transform);
                brick.transform.position = brickParent.transform.position + new Vector3(0, brickCount * 0.2f, 0);
                brick.transform.rotation = body.transform.rotation;
                brickObject.Add(brick);
                brickCount++;
                StartCoroutine(spawnBrick());
            }
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
    private void ReMoveBrick()
    {
        brickCount--;
        Destroy(brickObject[brickObject.Count - 1]);
        brickObject.RemoveAt(brickObject.Count - 1);
    }
    private bool CanMove(Vector3 point)
    {
        bool canMove = false;
        RaycastHit hit;
        if (Physics.Raycast(point, Vector3.down, 2f, groundLayer))
        {
            if (Physics.Raycast(point, Vector3.up, out hit, 7f, stairLayer)){
                if (hit.collider.GetComponent<Stair>().colorType != colorType && brickCount > 0)
                {
                    hit.collider.GetComponent<Stair>().ChangeColor(colorType);
                    ReMoveBrick();
                    canMove = true;
                }
                else if (hit.collider.GetComponent<Stair>().colorType == colorType)
                {
                    canMove = true;
                }
            }
            else canMove = true;
        }
        
        
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
