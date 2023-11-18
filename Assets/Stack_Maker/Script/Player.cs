using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public enum Direct { Forward, Back, Left,Right, None}
public class Player : MonoBehaviour
{
    [SerializeField] LayerMask layerBrick;
    [SerializeField] Transform playerBrichPrefab;
    [SerializeField] private float speed;
    [SerializeField] private Transform playerSkin;

    private Vector3 mouseDown, mouseUp;
    List<Transform> playerBricks = new List<Transform>();
    private Vector3 movePoint;

    public bool isMove;
    public Transform boxBrick;


    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    void OnInit()
    {
        isMove = false;
        clearBrick();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                mouseUp = Input.mousePosition;
                Direct direct = GetDirect(mouseDown, mouseUp);
                UnityEngine.Debug.Log(direct);
                if(direct != Direct.None)
                {
                    movePoint = GetnextPoint(direct);
                    UnityEngine.Debug.Log(movePoint);
                    isMove = true;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, movePoint)<0.1f)
            {
                isMove = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, movePoint, Time.deltaTime*speed);
        }

    }
    private Direct GetDirect(Vector3 mouseDown, Vector3 mouseUp)
    {
        Direct direct = Direct.None;
        float delayX = mouseUp.x - mouseDown.x;
        float delayY = mouseUp.y - mouseDown.y;
        if(Vector3.Distance(mouseDown, mouseUp) < 100)
        {
            direct = Direct.None;
        }
        else
        {
            if( Mathf.Abs(delayY) > Mathf.Abs(delayX))
            {
                if(delayY > 0)
                {
                    return Direct.Forward;
                }
                else
                {
                    return Direct.Back;
                }
            }
            else
            {
                if(delayX > 0)
                {
                    return Direct.Right;
                }
                else
                {
                    return Direct.Left;
                }
            }
        }
        return direct;
    }
    private Vector3 GetnextPoint(Direct direct)
    {

        RaycastHit hit;
        Vector3 nextPoint = transform.position;
        Vector3 dir = Vector3.zero;

        switch (direct)
        {
            case Direct.Forward:
                dir = Vector3.forward; break;
            case Direct.Back:
                dir = Vector3.back; break;
            case Direct.Left:
                dir = Vector3.left; break;
            case Direct.Right:
                dir = Vector3.right; break;
            case Direct.None:
                break;
            default:
                break;
        }

        for (int i = 1; i < 100; i++){
            if (Physics.Raycast(transform.position + dir*i+Vector3.up*2,Vector3.down, out hit,10f, layerBrick)) {
                nextPoint =  hit.collider.transform.position;
            }
            else
            {
                break;
            }
        }

        return nextPoint;
    }
    public void addBrick()
    {
        int index = playerBricks.Count;
        Transform playerBrick = Instantiate(playerBrichPrefab, boxBrick);
        playerBrick.localPosition = (index+1)*0.3f * Vector3.up;
        playerBricks.Add(playerBrick);
        playerSkin.localPosition = playerSkin.localPosition + Vector3.up*0.3f;
    }

    public void removeBrick()
    {
        int index = playerBricks.Count - 1;
        if(index >= 0)
        {
            Transform playerBrick = playerBricks[index];
            playerBricks.RemoveAt(index);
            Destroy(playerBrick.gameObject);
            playerSkin.localPosition = playerSkin.localPosition - Vector3.up * 0.3f;
        }
    }

    public void clearBrick()
    {
        for(int i = 0;i<playerBricks.Count; i++)
        {
            Destroy(playerBricks[i].gameObject);
        }
        playerBricks.Clear();
    }
        
}
