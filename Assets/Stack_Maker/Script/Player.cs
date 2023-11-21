using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Burst.CompilerServices;
using UnityEngine;


public enum Direct { Forward, Back, Left,Right, None}
public class Player : Character
{
    [SerializeField] private LayerMask layerBrick;    
    [SerializeField] private float speed;
    [SerializeField] private Transform playerSkin;
    [SerializeField] private LayerMask pushLayer;

    private Vector3 mouseDown, mouseUp;    
    private Vector3 movePoint;

    private bool isMove;
    private Direct currentDirect;


    // Start is called before the first frame update


    public override void OnInit()
    {
        base.OnInit();
        isMove = false;       

    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Debug.DrawRay(transform.position + Vector3.forward + Vector3.up, Vector3.down, Color.blue, 10f);
        if (!isMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                mouseUp = Input.mousePosition;
                currentDirect = GetDirect(mouseDown, mouseUp);
                //UnityEngine.Debug.Log(direct);
                if(currentDirect != Direct.None)
                {
                    movePoint = GetnextPoint(currentDirect);
                    //UnityEngine.Debug.Log(movePoint); 
                    isMove = true;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, movePoint)<0.1f)
            {
                             
                if (Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, 10f, pushLayer))
                {
                    UnityEngine.Debug.Log("push");
                     currentDirect =  getPushDirect(currentDirect);
                    movePoint = GetnextPoint(currentDirect);
                    
                }
                else
                {
                    
                    isMove=false;
                }
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
        changAnim(1);
        int index = playerBricks.Count;
        Transform playerBrick = Instantiate(playerBrichPrefab, boxBrick);
        playerBrick.localPosition = (index+1)*0.3f * Vector3.up;
        playerBricks.Add(playerBrick);
        playerSkin.localPosition = playerSkin.localPosition + Vector3.up*0.3f;
        Invoke(nameof(resetAnim), 0.3f);
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




    private Direct getPushDirect(Direct Direct)
    {
        
        isMove = true;
        if (Physics.Raycast(transform.position + Vector3.forward + Vector3.up * 2, Vector3.down, 10f, layerBrick) && Direct != Direct.Back)
        {
            return Direct.Forward;
        }
        if (Physics.Raycast(transform.position + Vector3.left + Vector3.up * 2, Vector3.down, 10f, layerBrick) && Direct != Direct.Right)
        {
            return Direct.Left;
        }
        if (Physics.Raycast(transform.position + Vector3.right + Vector3.up * 2, Vector3.down, 10f, layerBrick) && Direct != Direct.Left)
        {
            return Direct.Right;
        }
        if (Physics.Raycast(transform.position + Vector3.back+ Vector3.up * 2, Vector3.down, 10f, layerBrick)&& Direct != Direct.Forward)
        {
            return Direct.Back;
        }
        return Direct.None;

    }

    public void resetAnim()
    {
        changAnim(0);
    }


}





