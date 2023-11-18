using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private LayerMask roadLayer;
    bool isRunning = true;
    //[SerializeField] private LayerMask brickLayer;
    public enum RunningState
    {
        Up,
        Down,
        Left,
        Right,
        None
    }
    void Start()
    {
        
    }
    IEnumerator Move(RunningState state)
    {
        if (Check(state))
        {
            movetoState(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Move(state));
        }
        else if (Check(state))
        {
            movetoState(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Move(state));
        }
        else if (Check(state))
        {
            movetoState(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Move(state));
        }
        else if (Check(state))
        {
            movetoState(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Move(state));
        }
        else
        {
            isRunning = true;
            Debug.Log("stop");
        }
    }
    private void movetoState(RunningState state)
    {
        if (state == RunningState.Up) transform.position += new Vector3(0, 0, 1);
        else if (state == RunningState.Down) transform.position += new Vector3(0, 0, -1);
        else if (state == RunningState.Left) transform.position += new Vector3(-1, 0, 0);
        else if (state == RunningState.Right) transform.position += new Vector3(1, 0, 0);
        
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W)&&isRunning)
        {
            if(Check(RunningState.Up))
            {
                isRunning = false;
                Debug.Log("1");
                StartCoroutine(Move(RunningState.Up));
            } 
        }
        else if (Input.GetKeyDown(KeyCode.S)&&isRunning)
        {
            if(Check(RunningState.Down))
            {
                isRunning = false;
                Debug.Log("2");
                StartCoroutine(Move(RunningState.Down));
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) && isRunning)
        {
            if (Check(RunningState.Left))
            {
                isRunning = false;
                Debug.Log("3");
                StartCoroutine(Move(RunningState.Left));
            }
        }
        else if (Input.GetKeyDown(KeyCode.D)&&isRunning)
        {
            if (Check(RunningState.Right))
            {
                isRunning = false;
                Debug.Log("4");
                StartCoroutine(Move(RunningState.Right));
            }
        }
    }
    public bool Check(RunningState state)
    {
        if (state == RunningState.Up) return Physics.Raycast(up.transform.position, Vector3.down, 0.5f, roadLayer);
        else if(state == RunningState.Down) return Physics.Raycast(down.transform.position, Vector3.down, 0.5f, roadLayer);
        else if(state == RunningState.Left) return Physics.Raycast(left.transform.position, Vector3.down, 0.5f, roadLayer);
        else if(state == RunningState.Right) return Physics.Raycast(right.transform.position, Vector3.down, 0.5f, roadLayer);
        return false;
    }
}
