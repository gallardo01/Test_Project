using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private Transform up, down, left, right, center, body, brickHolder, endPoint;
    [SerializeField] private LayerMask brickLayer, roadLayer, pushLayer, lineLayer, BrickInBrickHolderLayer, finishedLineLayer, diamondLayer, brickOnLineLayer;
    [SerializeField] private GameObject Brick, BrickInBrickHolder, BrickOnLine, ClosedChest, OpenedChest, People;
    private int brickCount;
    private bool isRunning = true;
    private List<GameObject> listBrick = new List<GameObject>();
    public enum RunningState
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hell");
    }

    // Update is called once per frame
    void Update()
    {
        // changeDirection();

        //Debug.Log(Physics.Raycast(up.transform.position, Vector3.down, 5f, roadLayer));

        if (Input.GetKeyDown(KeyCode.W) && isRunning)
        {
            if (checkRunningState(RunningState.Up))
            {
                isRunning = false;
                StartCoroutine(PlayerRunning(RunningState.Up));
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) && isRunning)
        {
            if (checkRunningState(RunningState.Down))
            {
                isRunning = false;
                StartCoroutine(PlayerRunning(RunningState.Down));
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) && isRunning)
        {
            if (checkRunningState(RunningState.Left))
            {
                isRunning = false;
                StartCoroutine(PlayerRunning(RunningState.Left));
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) && isRunning)
        {
            if (checkRunningState(RunningState.Right))
            {
                isRunning = false;
                StartCoroutine(PlayerRunning(RunningState.Right));
            }
        }

        //Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.5f, Color.red);

        //Debug.DrawLine(center.position, Vector3.down * 0.25f, Color.red);

        //checkAddBrick();

        //checkMoveBrick();

    }


    private void moveToState(RunningState state)
    {
        if (state == RunningState.Up)
        {
            transform.position += new Vector3(1f, 0f, 0f);
        }
        else if (state == RunningState.Down)
        {
            transform.position += new Vector3(-1f, 0f, 0f);
        }
        else if (state == RunningState.Left)
        {
            transform.position += new Vector3(0f, 0f, 1f);
        }
        else if (state == RunningState.Right)
        {
            transform.position += new Vector3(0f, 0f, -1f);
        }
    }
    private bool checkRunningState(RunningState state)
    {
        if (state == RunningState.Up)
        {
            return Physics.Raycast(up.transform.position, Vector3.down, 5f, roadLayer) || Physics.Raycast(up.transform.position, Vector3.down, 5f, lineLayer);
        }
        else if (state == RunningState.Down)
        {
            return Physics.Raycast(down.transform.position, Vector3.down, 5f, roadLayer) || Physics.Raycast(down.transform.position, Vector3.down, 5f, lineLayer);
        }
        else if (state == RunningState.Left)
        {
            return Physics.Raycast(left.transform.position, Vector3.down, 5f, roadLayer) || Physics.Raycast(left.transform.position, Vector3.down, 5f, lineLayer);
        }
        else if (state == RunningState.Right)
        {
            return Physics.Raycast(right.transform.position, Vector3.down, 5f, roadLayer) || Physics.Raycast(right.transform.position, Vector3.down, 5f, lineLayer);
        }
        return false;
    }

    IEnumerator PlayerRunning(RunningState state)
    {
        if (checkRunningState(state))
        {
            Goal();
            popUpSign();
            CollectDiamond();
            checkAddBrick();
            //transform.position += new Vector3(0f,0f, 1f);
            moveToState(state);
            checkMoveBrick();
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(PlayerRunning(state));
        }
        else
        {
            if (checkOnPush())
            {
                Debug.Log("Stop1");
                StartCoroutine(PlayerRunning(returnPushState(state)));
            }
            else
            {
                isRunning = true;
                //StopAllCoroutines();
                Debug.Log("Stop");
            }
        }
    }


    //
    private RunningState returnPushState(RunningState currentState)
    {
        if (checkRunningState(RunningState.Up) && currentState != RunningState.Down)
        {
            return RunningState.Up;
        }
        else if (checkRunningState(RunningState.Down) && currentState != RunningState.Up)
        {
            return RunningState.Down;
        }
        else if (checkRunningState(RunningState.Left) && currentState != RunningState.Right)
        {
            return RunningState.Left;
        }
        else if (checkRunningState(RunningState.Right) && currentState != RunningState.Left)
        {
            return RunningState.Right;
        }
        return RunningState.None;
    }


    // add brick  -------------------------------------------------------------------
    private RaycastHit hit;
    private bool checkOnBrickState()
    {
        return Physics.Raycast(transform.position + new Vector3(0f, 1f), Vector3.down, out hit, Mathf.Infinity, brickLayer);
    }

    void addBrick()
    {
        Transform currentTransform = hit.collider.gameObject.transform;
        body.position = currentTransform.position + brickCount * new Vector3(0f, 0.25f);
        Destroy(hit.collider.gameObject);
        GameObject brick = Instantiate(BrickInBrickHolder, currentTransform.position + brickCount * new Vector3(0f, 0.25f), BrickInBrickHolder.transform.rotation, brickHolder);
        brickCount++;
        listBrick.Add(brick);
    }

    private void checkAddBrick()
    {
        if (checkOnBrickState())
        {  
            addBrick();
        }
    }


    // decrease brick  -------------------------------------------------------------------
    private RaycastHit hit2;
    private bool checkOnLineState()
    {
        return Physics.Raycast(transform.position + new Vector3(0f, 1f), Vector3.down, out hit2, Mathf.Infinity, lineLayer);
    }

    private RaycastHit hitBrick, hitLine;
    void removeBrick()
    {
        brickCount--;
        Transform currentTransform = hit2.collider.gameObject.transform;
        body.position = new Vector3(body.position.x, 0.35f + (brickCount -1) * 0.25f, body.position.z);
        Destroy(listBrick[listBrick.Count -1].gameObject);
        listBrick.RemoveAt(listBrick.Count - 1);
        Instantiate(BrickOnLine, currentTransform.position, BrickOnLine.transform.rotation);
        // if (Physics.Raycast(transform.position + new Vector3(0f, 1f), Vector3.down, out hitLine, Mathf.Infinity, lineLayer)) // 
        // {
        //     Debug.Log("Turn off collider");
        //     hitLine.collider.enabled = false;
        // }
    }

    private void checkMoveBrick()
    {
        if (checkOnLineState() && checkOnBrickState() == false)
        {  
            Debug.Log("removeBrick");
            removeBrick();
        }
    }


    // change direction  -------------------------------------------------------------------
    //private Vector3 currentEulerAngles;
    private RaycastHit hit3;
    private bool checkOnPush()
    {
        //Debug.DrawLine(transform.position + new Vector3(0f, 1f), transform.position + Vector3.down * 1f, Color.red);
        return Physics.Raycast(transform.position + new Vector3(0f, 1f), Vector3.down, out hit3, Mathf.Infinity, pushLayer);
    }


    //collect diamond -------------------------------------------------------------------
    private RaycastHit hitDiamond;
    private void CollectDiamond()
    {
        if (Physics.Raycast(center.position, Vector3.down, out hitDiamond, Mathf.Infinity, diamondLayer))
        {
            Debug.Log("Hit a diamond");
            Destroy(hitDiamond.collider.gameObject);
            UIController.Instance.UpDateScore();
        }
    }


    // check Goal -------------------------------------------------------------------
    private bool checkGoal()
    {
        return Physics.Raycast(transform.position + new Vector3(0f, 1f), Vector3.down, Mathf.Infinity, finishedLineLayer);
    }

    private void Goal()
    {
        if(checkGoal())
        {
            Instantiate(OpenedChest, ClosedChest.transform.position, ClosedChest.transform.rotation);
            Destroy(ClosedChest);
            //body.position = new Vector3(body.position.x, transform.position.y - 0.3f, body.position.z);
            Destroy(brickHolder.gameObject);
            brickCount = 0;
            People.transform.position = endPoint.position;
        }
    }


    //check check -------------------------------------------------------------------
    private bool checkOnBrickHolder()
    {
        return Physics.Raycast(transform.position + new Vector3(0f, 1f), Vector3.down, out hit, Mathf.Infinity, BrickInBrickHolderLayer);
    }

    private bool checkOnBrickOnLine()
    {
        return Physics.Raycast(transform.position + new Vector3(0f, 1f), Vector3.down, out hit, Mathf.Infinity, brickOnLineLayer);
    }


    //popup sign -------------------------------------------------------------------
    private bool checkAllBrick()
    {
        return (checkOnBrickState() == false) && (checkOnBrickHolder() == false) && (checkOnBrickOnLine() == false);
    }
    void popUpSign()
    {
        if (checkAllBrick())
        {
            UIController.Instance.lostSign();
        }
        if (checkGoal())
        {
            UIController.Instance.winSign();
        }
    }

    // void changeDirection()
    // {
    //     if (checkOnPush()) // && isRunning == true
    //     {
    //         body.eulerAngles = new Vector3(0f,-90f, 0f);
    //         //body.rotation = Quaternion.Euler(new Vector3(0f,-90f, 0f));
    //         hit3.collider.enabled = false;
    //         Debug.Log("Change Direction");
    //     }
    // }
    
}
