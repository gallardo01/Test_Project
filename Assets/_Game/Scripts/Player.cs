using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform pushRaycast;
    [SerializeField] private LayerMask roadLayer;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask pushLayer;
    [SerializeField] private GameObject brickHolder, brickPrefab, PlayerObject;
    private bool isRunning = true;

    private int brickCount = 0;

    public enum RunningState{
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3,
        None = 4
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(gameObject.transform.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.DrawLine(up.position, up.position + Vector3.down * 5f);
        // Debug.Log(Physics.Raycast(up.position, Vector3.down, 5f, roadLayer));
        if(Input.GetKeyDown(KeyCode.W) && isRunning){
            if(CheckRunningState(RunningState.Up)){
                // move
                // Debug.Log("Run");
                isRunning = false;
                StartCoroutine(PlayerRunning(RunningState.Up));
            }
        }
        else if(Input.GetKeyDown(KeyCode.S) && isRunning){
            if(CheckRunningState(RunningState.Down)){
                // move
                // Debug.Log("Run");
                isRunning = false;
                StartCoroutine(PlayerRunning(RunningState.Down));
            }
        }
        else if(Input.GetKeyDown(KeyCode.A) && isRunning){
            if(CheckRunningState(RunningState.Left)){
                // move
                // Debug.Log("Run");
                isRunning = false;
                StartCoroutine(PlayerRunning(RunningState.Left));
            }
        }
        else if(Input.GetKeyDown(KeyCode.D) && isRunning){
            if(CheckRunningState(RunningState.Right)){
                // move
                // Debug.Log("Run");
                isRunning = false;
                StartCoroutine(PlayerRunning(RunningState.Right));
            }
        }
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, brickLayer)){
            hit.collider.gameObject.SetActive(false);
            Transform currentTransform = hit.collider.gameObject.transform;
            Instantiate(brickPrefab, currentTransform.position + brickCount * new Vector3(0f,0.25f,0f), Quaternion.identity, brickHolder.transform);
            PlayerObject.transform.position = currentTransform.position + brickCount * new Vector3(0f,0.25f,0f) + new Vector3(0f,0f,0f);
            brickCount++;
        }
        if(Physics.Raycast(pushRaycast.position, Vector3.down, out hit, Mathf.Infinity, pushLayer)){
            Debug.Log("Push");
            moveWithState(hit.collider.gameObject.GetComponent<Push>().stateMove);
        }
    }

    // private RunningState PushState(RunningState state){
        // if(state == RunningState.Up){
        //     return 
        // }
        // else if(state == RunningState.Down){
        //     return 
        // }
        // else if(state == RunningState.Left){
        //     return 
        // }
        // else if(state == RunningState.Right){
        //     return 
        // }
    // }

    private void moveWithState(int direct){
        isRunning = false;
        RunningState state = RunningState.None;
        if(direct == 0){
            state = RunningState.Up;
        }
        if(direct == 1){
            state = RunningState.Down;
        }
        if(direct == 2){
            state = RunningState.Left;
        }
        if(direct == 3){
            state = RunningState.Right;
        }
        if(direct == 4){
            state = RunningState.None;
        }
        StartCoroutine(PlayerRunning(state));
    }

    IEnumerator PlayerRunning(RunningState state){
        if(CheckRunningState(state)){
            // transform.position += new Vector3(0f, 0f, -1f);
            moveToState(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(PlayerRunning(state));
        }
        else{
            isRunning = true;
            Debug.Log("Stop");
        }
    }

    private void moveToState(RunningState state){
        if(state == RunningState.Up){
            transform.position += new Vector3(0f, 0f, -1f);
        }
        else if(state == RunningState.Down){
            transform.position += new Vector3(0f, 0f, 1f);
        }
        else if(state == RunningState.Left){
            transform.position += new Vector3(1f, 0f, 0f);
        }
        else if(state == RunningState.Right){
            transform.position += new Vector3(-1f, 0f, 0f);
        }
    }

    private bool CheckRunningState(RunningState state){
        // Direction = 1: Up
        if(state == RunningState.Up){
            return Physics.Raycast(up.position, Vector3.down, Mathf.Infinity, roadLayer);
        }
        else if(state == RunningState.Down){
            return Physics.Raycast(down.position, Vector3.down, Mathf.Infinity, roadLayer);
        }
        else if(state == RunningState.Left){
            return Physics.Raycast(left.position, Vector3.down, Mathf.Infinity, roadLayer);
        }
        else if(state == RunningState.Right){
            return Physics.Raycast(right.position, Vector3.down, Mathf.Infinity, roadLayer);
        }
        return false;
    }
}
