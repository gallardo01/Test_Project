using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private Transform up, down, left, right, center, body, brickHolder;
    [SerializeField] private LayerMask brickLayer,roadLayer, pushLayer, lineLayer;
    [SerializeField] private GameObject Brick;
    private int brickCount;
    private bool isRunning = true;
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

        Debug.Log(Physics.Raycast(up.transform.position, Vector3.down, 5f, roadLayer));

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

        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.5f, Color.red);

        checkAddBrick();

        checkMoveBrick();

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
            //transform.position += new Vector3(0f,0f, 1f);
            moveToState(state);
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
        Instantiate(Brick, currentTransform.position + brickCount * new Vector3(0f, 0.25f), Quaternion.identity, brickHolder);
        brickCount++;
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

    private RaycastHit hitBrick;
    void removeBrick() // them layer gach2 vao gach trong brickholder
    {
        Transform currentTransform = hit2.collider.gameObject.transform;
        body.position = currentTransform.position + brickCount * new Vector3(0f, 0.25f);
        Physics.Raycast(transform.position + new Vector3(0f, 1f), Vector3.down, out hitBrick, 0.25f, brickLayer);
        if (Physics.Raycast(transform.position + new Vector3(0f, 1f), Vector3.down, out hitBrick, 0.25f, brickLayer)) // Highest brick
        {
            Destroy(hitBrick.collider.gameObject);
        }
        Instantiate(Brick, currentTransform.position, Quaternion.identity);
        brickCount--;
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

    void changeDirection()
    {
        if (checkOnPush()) // && isRunning == true
        {
            body.eulerAngles = new Vector3(0f,-90f, 0f);
            //body.rotation = Quaternion.Euler(new Vector3(0f,-90f, 0f));
            hit3.collider.enabled = false;
            Debug.Log("Change Direction");
        }
    }
}