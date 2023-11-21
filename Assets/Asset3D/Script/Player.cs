using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float speed;
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private LayerMask roadLayer;
    [SerializeField] private LayerMask whiteLayer;
    [SerializeField] private LayerMask pushLayer;
    [SerializeField] private GameObject player;
    bool isRunning = true;
    bool a, b, c, d;
    //[SerializeField] private LayerMask brickLayer;
    public enum RunningState
    {
        Up,
        Down,
        Left,
        Right,
        None
    }
    IEnumerator Move(RunningState state)
    {
        if (Check(state) || Check1(state) || Check3(state))
        {
            movetoState(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Move(state));
        }
        else if (Check(state) || Check1(state) || Check3(state))
        {
            movetoState(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Move(state));
        }
        else if (Check(state) || Check1(state) || Check3(state))
        {
            movetoState(state);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Move(state));
        }
        else if (Check(state) || Check1(state) || Check3(state))
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
        BrickStack.Instance.check();
        player.transform.position = new Vector3(transform.position.x, transform.position.y  + 0.25f * BrickStack.Instance.brick,transform.position.z);
        BrickStack.Instance.check1();
        if (state == RunningState.Up) transform.position += new Vector3(0, 0, 1);
        else if (state == RunningState.Down) transform.position += new Vector3(0, 0, -1);
        else if (state == RunningState.Left) transform.position += new Vector3(-1, 0, 0);
        else if (state == RunningState.Right) transform.position += new Vector3(1, 0, 0);
    }

    void Update()
    {
        a = Input.GetKeyDown(KeyCode.W);
        b  = Input.GetKeyDown(KeyCode.S);
        c = Input.GetKeyDown(KeyCode.A);
        d = Input.GetKeyDown(KeyCode.D);
        Check2();
        if (a&&isRunning)
        {
            if(Check(RunningState.Up) || Check1(RunningState.Up) ||Check3(RunningState.Up))
            {
                isRunning = false;
                Debug.Log("1");
                StartCoroutine(Move(RunningState.Up));
            } 
        }
        else if (b && isRunning)
        {
            if(Check(RunningState.Down) || Check1(RunningState.Down) || Check3(RunningState.Down))
            {
                isRunning = false;
                Debug.Log("2");
                StartCoroutine(Move(RunningState.Down));
            }
        }
        else if (c && isRunning)
        {
            if (Check(RunningState.Left) || Check1(RunningState.Left) || Check3(RunningState.Left))
            {
                isRunning = false;
                Debug.Log("3");
                StartCoroutine(Move(RunningState.Left));
            }
        }
        else if (d && isRunning)
        {
            if (Check(RunningState.Right) || Check(RunningState.Right) || Check3(RunningState.Right))
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
    public bool Check1(RunningState state)
    {
        if (state == RunningState.Up) return Physics.Raycast(up.transform.position, Vector3.down, 0.3f, pushLayer);
        else if (state == RunningState.Down) return Physics.Raycast(down.transform.position, Vector3.down, 0.3f, pushLayer);
        else if (state == RunningState.Left) return Physics.Raycast(left.transform.position, Vector3.down, 0.3f, pushLayer);
        else if (state == RunningState.Right) return Physics.Raycast(right.transform.position, Vector3.down, 0.3f, pushLayer);
        return false;
    }
    void Check2()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, 0.5f*Vector3.down,Color.red);
        if (Physics.Raycast(transform.position, Vector3.down,out hit, 0.3f, pushLayer))
        {
            if (hit.collider.transform.localRotation == Quaternion.Euler(0f, -90f, 0f))
            {
                d = true;
            }
            else if (hit.collider.transform.localRotation == Quaternion.Euler(0f, 90f, 0f)|| hit.collider.transform.localRotation == Quaternion.Euler(0f, 180f, 0f))
            {
                a = true;
            }
            else if (hit.collider.transform.localRotation == Quaternion.Euler(0f, 0f, 0f))
            {
                Debug.Log(true);
                c = true;
            }
        } 
    }
    public bool Check3(RunningState state)
    {
        if (state == RunningState.Up) return Physics.Raycast(up.transform.position, Vector3.down, 0.5f, whiteLayer);
        else if (state == RunningState.Down) return Physics.Raycast(down.transform.position, Vector3.down, 0.5f, whiteLayer);
        else if (state == RunningState.Left) return Physics.Raycast(left.transform.position, Vector3.down, 0.5f, whiteLayer);
        else if (state == RunningState.Right) return Physics.Raycast(right.transform.position, Vector3.down, 0.5f, whiteLayer);
        return false;
    }
}
