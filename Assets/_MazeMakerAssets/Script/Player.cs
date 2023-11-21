using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform center;
    [SerializeField] private Transform body;
    [SerializeField] private LayerMask roadLayer;
    [SerializeField] private LayerMask purpleLayer;

    private RunningState currentState;
    private bool isRunning = true;

    public enum RunningState
    {
        Up,
        Down,
        Left,
        Right,
        Center,
        None
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Physics.Raycast(up.transform.position, Vector3.down, 5f, roadLayer);
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
    }

    IEnumerator PlayerRunning(RunningState state)
    {
        if (checkRunningState(state))
        {
            // Move

            // Nhat?
            // Tao ra gach, thay doi toa do nhan vat
            moveToState(state);
            currentState = state;
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(PlayerRunning(state));
        }
        else
        {
            // stop
            // check tim
            if (Physics.Raycast(center.transform.position, Vector3.down, 5f, purpleLayer))
            {
                // tim huong?
               Debug.Log("Gap tim");
                //Debug.Log(returnPurpleState());

                StartCoroutine(PlayerRunning(returnPurpleState()));
            }
            else
            {
                isRunning = true;
            }
            Debug.Log("Stop");
        }
    }

    private RunningState returnPurpleState()
    {
        if (currentState != RunningState.Down)
        {
            if (checkRunningState(RunningState.Up)){
                return RunningState.Up;
            }
        }
        if (currentState != RunningState.Up)
        {
            if (checkRunningState(RunningState.Down))
            {
                return RunningState.Down;
            }
        }
        if (currentState != RunningState.Left)
        {
            if (checkRunningState(RunningState.Left))
            {
                return RunningState.Left;
            }
        }
        if (currentState != RunningState.Right)
        {
            if (checkRunningState(RunningState.Right))
            {
                return RunningState.Right;
            }
        }
        return RunningState.None;
    }

    private void moveToState(RunningState state)
    {
        if (state == RunningState.Up)
        {
            transform.position += new Vector3(0f, 0f, 1f);
        }
        else if (state == RunningState.Down)
        {
            transform.position += new Vector3(0f, 0f, -1f);
        }
        else if (state == RunningState.Left)
        {
            transform.position += new Vector3(-1f, 0f, 0f);
        }
        else if (state == RunningState.Right)
        {
            transform.position += new Vector3(1f, 0f, 0f);
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
        else if (state == RunningState.Center)
        {
            return Physics.Raycast(center.transform.position, Vector3.down, 5f, roadLayer);
        }
        return false;
    }
}
