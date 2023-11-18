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

    private Vector3 direction;
    private Vector3 oldDirection;
    private int currentIndex;

    public Vector3 Direction { get => direction; }

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        oldDirection = Vector3.zero;
        direction = Vector3.zero;
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
        }
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
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
}
