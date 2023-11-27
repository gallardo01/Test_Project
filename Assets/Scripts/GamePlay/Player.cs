using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.UIElements;
using UnityEngine;

enum eDirection
{
    forward = 0,
    back = 1,
    left = 2,
    right = 3,
}

public class Player : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Transform[] rayCastTransform;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask pushLayer;
    [SerializeField] private Transform push;
    [SerializeField] private LayerMask finishLine;
    [SerializeField] private Animator animator;

    private Vector3 direction;
    private Vector3 oldDirection;
    private String currentAnimName;
    private int currentIndex;
    private bool animCalled;

    public Vector3 Direction { get => direction; }

    private void ChangeAnim(String animName) {
        animator.ResetTrigger(currentAnimName);

        currentAnimName = animName;

        animator.SetTrigger(currentAnimName);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentAnimName = "";
        animCalled = false;
        currentIndex = 0;
        oldDirection = Vector3.zero;
        direction = Vector3.zero;
        Sub();
    }

    private bool checkWall(Transform start)
    {
        return Physics.Raycast(start.position + direction * 0.01f, Vector3.down, 2f, wallLayer);
    }

    private float roundToHalf(float value)
    {
        return Mathf.RoundToInt(value * 2) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkWall(rayCastTransform[currentIndex]))
        {
            oldDirection = direction;
            direction = Vector3.zero;
            transform.position = new Vector3(roundToHalf(transform.position.x), transform.position.y, roundToHalf(transform.position.z));
            if (Physics.Raycast(push.position, Vector3.down, 2, pushLayer))
            {
                Turn();
            }
            if (direction == Vector3.zero && !animCalled) {
                ChangeAnim("Stop");
                animCalled = true;
            }
        }

        if (Physics.Raycast(transform.position, Vector3.down, 1f, finishLine)) {
            direction = Vector3.zero;
            transform.position = new Vector3(roundToHalf(transform.position.x), transform.position.y, roundToHalf(transform.position.z));
            ParticleSystemController.Instance.Celebrate();
            transform.position += Vector3.forward * 2;
            transform.position -= Vector3.down * 0.5f;
            StackController.Instance.Finish();
            ChangeAnim("Celebrate");
        }

        if (direction != Vector3.zero) animCalled = false;


        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void Stop() {
        direction = Vector3.zero;
    }

    public void Turn()
    {
        for (int i = 0; i < rayCastTransform.Count<Transform>(); i++)
        {
            if (!checkWall(rayCastTransform[i]) && rayCastTransform[i].localPosition.normalized != (-oldDirection) && rayCastTransform[i].localPosition.normalized != oldDirection)
            {
                direction = -rayCastTransform[i].localPosition.normalized;
                currentIndex = i;
                break;
            }
        }
    }

    public void MoveUp()
    {
        currentIndex = (int)eDirection.forward;
        direction = Vector3.forward;
    }

    public void MoveBack()
    {
        currentIndex = (int)eDirection.back;
        direction = Vector3.back;
    }

    public void MoveLeft()
    {
        currentIndex = (int)eDirection.left;
        direction = Vector3.left;
    }

    public void MoveRight()
    {
        currentIndex = (int)eDirection.right;
        direction = Vector3.right;
    }

    private void Sub() {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");

        foreach (var brick in bricks)
        {
            brick.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", CurrentTile.Instance.currentTexture);
        }
    }
}
