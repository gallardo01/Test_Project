using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private Transform up, down, left, right, body, brickHolder;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask roadLayer;
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

        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.1f, Color.red);

        checkAddBrick();
    }

    private void moveToState(RunningState state)
    {
        if (state == RunningState.Up)
        {
            transform.position += new Vector3(0.5f, 0f, 0f);
        }
        else if (state == RunningState.Down)
        {
            transform.position += new Vector3(-0.5f, 0f, 0f);
        }
        else if (state == RunningState.Left)
        {
            transform.position += new Vector3(0f, 0f, 0.5f);
        }
        else if (state == RunningState.Right)
        {
            transform.position += new Vector3(0f, 0f, -0.5f);
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
}
