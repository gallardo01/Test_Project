using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class PlayerController : Character
{
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform center;
    [SerializeField] private Transform body;

    [SerializeField] private LayerMask roadLayer;
    [SerializeField] private LayerMask purpleLayer;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask whileLayer;

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

        Physics.Raycast(up.transform.position, Vector3.down, 5f,roadLayer);
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
            //Pick brick
            RaycastHit hit;
            Debug.Log(Physics.Raycast(center.transform.position, Vector3.down, out hit,5f,brickLayer));
            if(Physics.Raycast(center.transform.position, Vector3.down, out hit,5f,brickLayer))
            {
                totalStack++;
                GameObject brick = hit.collider.gameObject;
                brick.layer = LayerMask.NameToLayer("Default");
                brick.transform.SetParent(brickParent);
                brick.transform.localPosition = new Vector3(0F,(totalStack-1)*0.25f,0f);
                playerBody.transform.localPosition = new Vector3(0F,(totalStack-1)*0.25f,0f);
                playerBricks.Add(brick);
            }
            if (Physics.Raycast(center.transform.position, Vector3.down, out hit, 5f, whileLayer))
            {
                Debug.Log(totalStack);
                if (totalStack > 0)
                {
                    GameObject white = hit.collider.gameObject;
                    GameObject destroyBrick = playerBricks[totalStack - 1];
                    destroyBrick.transform.SetParent(white.transform);
                    destroyBrick.transform.localPosition = new Vector3(0f, 0f, -1.035f);
					playerBricks.RemoveAt(playerBricks.Count - 1);
                    totalStack--;
                    
					playerBody.transform.localPosition = new Vector3(0f, (totalStack - 1) * 0.25f, 0f);
					Destroy(destroyBrick);
				} else
                {
                }
            }
            // Move
            moveToState(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(PlayerRunning(state));
        }
        else
        {
            //check purle
            if(Physics.Raycast(center.transform.position, Vector3.down, 5f,purpleLayer))
            {
                StartCoroutine(PlayerRunning(returnPurpleSate(state)));
            }
            else
            {
                // stop
                isRunning = true;
                Debug.Log("Stop");
            }
        }
    }

    // private void removeBrick()
    // {
    //     GameObject while = hit.collider.gameObject;
        
    //     GameObject brick = playerBricks[playerBricks.Count - 1];
    //     brick.transform.SetParent(while.transform);
    //     brick.transform.localPosition = new Vector3(0f, 0f, -0.3f);
    //     playerBricks.RemoveAt(playerBricks.Count - 1);
    // }

    private RunningState returnPurpleSate(RunningState currentState)
    {
        if (checkRunningState(RunningState.Up) && currentState != RunningState.Down)
        {
            return RunningState.Up;
        }
        else if(checkRunningState(RunningState.Down) && currentState != RunningState.Up)
        {
            return RunningState.Down;

        }
        else if(checkRunningState(RunningState.Left) && currentState != RunningState.Right)
        {
            return RunningState.Left;

        }
        else if(checkRunningState(RunningState.Right) && currentState != RunningState.Left)
        {
            return RunningState.Right;
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
