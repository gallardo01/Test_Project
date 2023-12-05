using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { None, Up, Down, Left, Right }

public class Player : MonoBehaviour
{
    [SerializeField] private Stacker stacker;
    [SerializeField] private Animator animator;
    [SerializeField] private float checkDistance = 1f;
    [SerializeField] private LayerMask movableLayer;
    [SerializeField] private float speed = 10f;

    [HideInInspector] public Direction currentDir;
    
    private Direction _stashDir;
    private Vector3 _nextPosition;
    private Color _color = Color.green;
    private bool _onWin;

    private Vector3 PositionXZ => new (transform.position.x, 0, transform.position.z);
    
    public void IncrementHeight(int increment) => stacker.IncrementHeight(increment);
    
    private void Start()
    {
        _nextPosition = transform.position;
    }

    public void OnWin()
    {
        _onWin = true;
        animator.SetTrigger("Win");
    }

    void Update()
    {
        if (_onWin) return;
        
        if (currentDir == Direction.None)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                currentDir = Direction.Up;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                currentDir = Direction.Left;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                currentDir = Direction.Down;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                currentDir = Direction.Right;
            }
        }

        if (currentDir == Direction.None) return;
        
        CheckForward();
        Move();
    }

    public void StashDirection(Direction dir)
    {
        _stashDir = dir;
    }

    private void Move()
    {
        if (Vector3.Distance(PositionXZ, _nextPosition) > 0.1f)
        {
            Debug.Log("Moving");
            animator.SetBool("IsMoving", true);
            var targetPos = _nextPosition;
            targetPos.y = transform.position.y;
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                Time.deltaTime * speed);
        }
        else
        {
            if (_stashDir != Direction.None)
            {
                currentDir = _stashDir;
                _stashDir = Direction.None;
                return;
            }
            
            animator.SetBool("IsMoving", false);
            Debug.Log("Stopped Moving");
            currentDir = Direction.None;
        }
    }

    private void CheckForward()
    {
        Vector3 checkOrigin = GetForwardCheck(currentDir);
        checkOrigin.y += 10;
        if (Physics.Raycast(checkOrigin, Vector3.down, out RaycastHit hit, 100))
        {
            if (movableLayer.ContainLayer(hit.collider.gameObject.layer))
            {
                _nextPosition = new Vector3(hit.collider.transform.position.x, 0, hit.collider.transform.position.z);
                _color = Color.green;
                return;
            }
        }

        _color = Color.red;
    }
    
    private Vector3 GetForwardCheck(Direction direction)
    {
        Vector3 check = transform.position;
        switch (direction)
        {
            case Direction.None:
                break;
            case Direction.Up:
                check.z += checkDistance;
                break;
            case Direction.Down:
                check.z -= checkDistance;
                break;
            case Direction.Left:
                check.x -= checkDistance;
                break;
            case Direction.Right:
                check.x += checkDistance;
                break;
        }

        return check;
    }

    private void OnDrawGizmos()
    {
        if (currentDir == Direction.None) return;
        
        Vector3 origin = GetForwardCheck(currentDir); 
        
        Vector3 pointA = origin;
        Vector3 pointB = origin;
        
        pointA.y += 10;
        pointB.y -= 100;
        
        Gizmos.color = _color;
        Gizmos.DrawLine(pointA, pointB);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IMovable movable))
        {
            movable.OnHit(this);
        }
        else
        {
            Debug.Log("Does not contain IMovable");
        }
    }
}
