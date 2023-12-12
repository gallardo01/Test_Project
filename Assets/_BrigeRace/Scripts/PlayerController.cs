using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayerController : Character
{
    // public float speed = 5f;
    // public Animator animator;
    // private string currentAnim;
    // public Transform body;
    // public LayerMask groundLayer;
    // public LayerMask stairLayer;
    // public Stage stage;

    // [SerializeField] PlayerBrick playerBrickPrefabs;
    // [SerializeField] Transform brickHolder;
    // private List<PlayerBrick> playerBricks = new List<PlayerBrick>();
    // [SerializeField] private Transform endPoint;

    // Start is called before the first frame update
    void Start()
    {
        changeAnim("idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !LevelManager.Ins.isWin)
        {
            //transform.position += JoystickControl.direct * speed * Time.deltaTime;

            Vector3 nextPoints = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            if (CanMove(nextPoints))
            {
                transform.position = checkGround(nextPoints);
            }
            // Change anim
            changeAnim("run");
            if (JoystickControl.direct != Vector3.zero)
            {
                body.forward = JoystickControl.direct;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            changeAnim("idle");
        }
        if(LevelManager.Ins.isWin){
            // transform.position = Vector3.MoveTowards(transform.position, endPoint.position, 0.1f);
        }
    }

    // private bool CanMove(Vector3 point)
    // {
    //     bool canMove = false;
    //     if (Physics.Raycast(point + Vector3.up, Vector3.down, 5f, groundLayer))
    //     {
    //         RaycastHit hit;
    //         if (Physics.Raycast(point + Vector3.up, Vector3.down, out hit, 5f, stairLayer))
    //         {
    //             Debug.Log("stair layer");
    //             if (hit.collider.gameObject.GetComponent<ColorObject>().colorType == colorType)
    //             {
    //                 Debug.Log("same color");
    //                 canMove = true;
    //             } else
    //             {
    //                 Debug.Log("different color");
    //                 if (playerBricks.Count > 0)
    //                 {
    //                     hit.collider.GetComponent<Stair>().ChangeColor(colorType);
    //                     RemoveBrick();
    //                 }
    //                 canMove = false;
    //             }
    //         } else {
    //             Debug.Log("ground");
    //             canMove = true;
    //         }
    //     }

        
    //     return canMove;
    // }

    // private Vector3 checkGround(Vector3 point)
    // {
    //     RaycastHit hit;
    //     if (Physics.Raycast(point + Vector3.up, Vector3.down, out hit, 5f, groundLayer))
    //     {
    //         return hit.point + Vector3.up * 0.3f;
    //     }
    //     return point;
    // }

    // public void changeAnim(string animName)
    // {
    //     if (currentAnim != animName)
    //     {
    //         animator.ResetTrigger(currentAnim);
    //         currentAnim = animName;
    //         animator.SetTrigger(currentAnim);
    //     }
    // }

    // public void AddBrick()
    // {
    //     PlayerBrick playerBrick = Instantiate(playerBrickPrefabs, brickHolder);
    //     playerBrick.ChangeColor(colorType);
    //     playerBrick.transform.localPosition = new Vector3(0f, 0.2f * (playerBricks.Count-1), 0f);
    //     playerBricks.Add(playerBrick);
    // }

    // public void RemoveBrick()
    // {
    //     if (playerBricks.Count > 0)
    //     {
    //         PlayerBrick playerBrick = playerBricks[playerBricks.Count - 1];
    //         playerBricks.RemoveAt(playerBricks.Count - 1);
    //         Destroy(playerBrick.gameObject);
    //     }
    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.tag == "Brick")
    //     {
    //         if (other.gameObject.GetComponent<ColorObject>().colorType == colorType)
    //         {
    //             stage.RemoveBrick(other.GetComponent<Brick>());
    //             AddBrick();
    //         }
    //     }
    // }
}
