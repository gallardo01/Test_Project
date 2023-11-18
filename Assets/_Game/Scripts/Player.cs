using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform body;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask roadLayer;
    [SerializeField] private GameObject Brick;
    //private float speed = 5;
    private bool isGrounded = true;
    private bool isRunning = true;
    public enum RunningState
    {
        Up,
        Down,
        Left,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Physics.Raycast(up.transform.position, Vector3.down, 5f, roadLayer));

        // if (Input.GetKeyDown(KeyCode.W) && isRunning)
        // {
        //     if (checkRunningState(RunningState.Up))
        //     {
        //         isRunning = false;
        //         StartCoroutine(PlayerRunning(RunningState.Up));
        //     }
        // }
        // else if (Input.GetKeyDown(KeyCode.S) && isRunning)
        // {
        //     if (checkRunningState(RunningState.Down))
        //     {
        //         isRunning = false;
        //         StartCoroutine(PlayerRunning(RunningState.Down));
        //     }
        // }
        // else if (Input.GetKeyDown(KeyCode.A) && isRunning)
        // {
        //     if (checkRunningState(RunningState.Left))
        //     {
        //         isRunning = false;
        //         StartCoroutine(PlayerRunning(RunningState.Left));
        //     }
        // }
        // else if (Input.GetKeyDown(KeyCode.D) && isRunning)
        // {
        //     if (checkRunningState(RunningState.Right))
        //     {
        //         isRunning = false;
        //         StartCoroutine(PlayerRunning(RunningState.Right));
        //     }
        // }
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.1f, Color.red);

        if (checkOnBrickState())
        {
            addBrick();
            Debug.Log("Add Brick");
        }
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
                isRunning = true;
                //StopAllCoroutines();
                Debug.Log("Stop");
            }
    }

    private bool checkRunningState(RunningState state)
    {
        if (state == RunningState.Up)
        {
            return Physics.Raycast(up.transform.position, Vector3.down, 5f, roadLayer);
        }
        else if (state == RunningState.Down)
        {
            return Physics.Raycast(down.transform.position, Vector3.down, 5f, roadLayer);
        }
        else if (state == RunningState.Left)
        {
            return Physics.Raycast(left.transform.position, Vector3.down, 5f, roadLayer);
        }
        else if (state == RunningState.Right)
        {
            return Physics.Raycast(right.transform.position, Vector3.down, 5f, roadLayer);
        }
        return false;
    }

    private bool checkOnBrickState()
    {
        return Physics.Raycast(transform.position, Vector3.down, 5f, brickLayer);
    }

    void addBrick()
    {
        Instantiate(Brick, body.transform.position + new Vector3(0f, 0.25f), Quaternion.identity);
        //Destroy();
    }
}
