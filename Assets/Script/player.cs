﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private float speedPlayer = 2f;
    [SerializeField] private float distance = 0.3f;
    [SerializeField] private float timeLoop = 0.0f;
    [SerializeField] private Rigidbody2D rb; 
    [SerializeField] private LayerMask endLayer;
    [SerializeField] private float moveTime;
    [SerializeField] private Animator anim;

    public GameObject end;
    public GameObject end1;
    public GameObject end2;
    public GameObject start;
    public float journeyTime = 1.0f;
     
    private float speed;
    private Vector2 currentDirection;
    private int countPoint = -1;
    private Vector3 target;
    private bool IsMoving = true;
    private float startTime;
    private string currentAnim;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = start.transform.position;
        //3
        //target = end1.transform.position;
        //4
        //startTime = Time.time;
        //5
        //target = end.transform.position;
        //7
        //changeDirection();
        //8
        target = end.transform.position;

        //9
        //target = end.transform.position;
        //float dis = Vector2.Distance(transform.position, target);
        //speed = dis / moveTime; 

        //10
        //target = end.transform.position;


    }
    
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(Vector3.right * speedPlayer * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Translate(Vector3.left * speedPlayer * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.Translate(Vector3.down * speedPlayer * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.Translate(Vector3.up * speedPlayer * Time.deltaTime);
        //}

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical,0).normalized;
        
        if (movement != Vector3.zero)
        {
            if (!isMoving)
            {
                ChangeAnim("run");
                isMoving = true;
            }
            transform.rotation = Quaternion.Euler(0, horizontal > 0 ? 0 : 180, 0);
            rb.velocity = new Vector2(horizontal * speedPlayer, vertical * speedPlayer);
        }
        else
        {
            if (isMoving)
            {
                ChangeAnim("idle");
                isMoving = false;
            }
        }

        //2
        //while (Vector3.Distance(transform.position, end.transform.position) < distance)
        //{
        //    transform.position = start.transform.position;
        //}

        //3
        //transform.position = Vector3.MoveTowards(transform.position, target, speedPlayer * Time.deltaTime);

        //if (Vector2.Distance(transform.position, end1.transform.position) < 0.1f)
        //{
        //    target = end2.transform.position;
        //}
        //if (Vector2.Distance(transform.position, end2.transform.position) < 0.1f)
        //{
        //    target = end.transform.position;
        //}
        //if (Vector2.Distance(transform.position, end.transform.position) < 0.1f)
        //{
        //    target = start.transform.position;

        //}
        //if (Vector2.Distance(transform.position, start.transform.position) < 0.1f)
        //{
        //    target = end1.transform.position;

        //}


        //4
        //Vector3 center = (start.transform.position - end.transform.position) * 0.5f;
        //center -= new Vector3(0, 3 , 0);
        //Vector3 riseRelCenter = start.transform.position - center;
        //Vector3 setRelCenter = end.transform.position - center;
        //float fracComplete = (Time.time - startTime) / journeyTime;

        //transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        //transform.position += center;


        //5
        //transform.position = Vector3.MoveTowards(transform.position, target, speedPlayer * Time.deltaTime);

        //if (Vector2.Distance(transform.position, end.transform.position) < 0.1f)
        //{
        //    target = start.transform.position;
        //}
        //if (Vector2.Distance(transform.position, start.transform.position) < 0.1f)
        //{
        //    target = end.transform.position;
        //}


        //7
        //if(countPoint == 3)
        //{
        //    rb.velocity = Vector2.zero;
        //}
        //else
        //{
        //    transform.Translate(currentDirection * speedPlayer * Time.deltaTime);
        //    timeLoop += Time.deltaTime;
        //    if (timeLoop > Random.Range(2f, 3.5f))
        //    {
        //        changeDirection();
        //    }
        //}

        //8
        //timeLoop += Time.deltaTime;
        //if (timeLoop - 1 > 0.1f)
        //{
        //    IsMoving = !IsMoving;
        //    timeLoop = 0f;
        //}
        //if (IsMoving)
        //{

        //    transform.position = Vector3.Lerp(transform.position, target,speedPlayer);
        //    transform.position = Vector3.MoveTowards(transform.position, target, speedPlayer * Time.deltaTime);
        //}
        //else
        //{
        //    rb.velocity = Vector2.zero;
        //}

        //if (Vector2.Distance(transform.position, end.transform.position) < 0.1f)
        //{
        //    target = start.transform.position;
        //}
        //if (Vector2.Distance(transform.position, start.transform.position) < 0.1f)
        //{
        //    target = end.transform.position;
        //}

        //9
        //float distanceCurrent = Vector2.Distance(transform.position, target);       
        //transform.position = Vector2.Lerp(transform.position, target, speed/distanceCurrent*Time.deltaTime);


        //10
        //transform.position = Vector3.MoveTowards(transform.position, target, speedPlayer * Time.deltaTime);
        //if (Vector2.Distance(transform.position, end.transform.position) < 0.1f)
        //{

        //    StartCoroutine(wait(start.transform.position));
        //    //end.transform.position
        //}
        //if (Vector2.Distance(transform.position, start.transform.position) < 0.1f)
        //{

        //    StartCoroutine(wait(end.transform.position));
        //    //target = end.transform.position;
        //}

        //11
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    Debug.Log("click");
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Debug.Log("va cham");
        //        if (hit.collider.tag == "player")
        //        {
        //            GetComponent<Renderer>().material.color = Color.red;
        //        }
        //    }
        //}

        //12
        //Debug.DrawLine(transform.position, transform.position + Vector3.right * 0.5f, Color.blue);
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, endLayer);
        //if (hit.collider != null)
        //{
        //    Debug.Log("complete");
        //}


    }
    IEnumerator wait(Vector3 vec)
    {
        yield return new WaitForSeconds(Random.Range(2f,4f));
        target = vec;
        Debug.Log("wait");
    }
    void changeDirection()
    {
        currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        timeLoop = 0.0f;
        countPoint++;
    }

    private void ChangeAnim(string nameAnim)
    {
        if(currentAnim != nameAnim)
        {
            anim.ResetTrigger(nameAnim);
            currentAnim = nameAnim;
            anim.SetTrigger(currentAnim);
        }
    }
}