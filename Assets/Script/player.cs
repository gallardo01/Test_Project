using System.Collections;
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

    public GameObject end;
    public GameObject end1;
    public GameObject end2;
    public GameObject start;

    private float speed;
    private Vector2 currentDirection;
    private int countPoint = -1;
    private Vector3 target;
    private bool IsMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = start.transform.position;
        //3
        target = end1.transform.position;
        //5
        //target = end.transform.position;
        //7
        //changeDirection();
        //8
        //target = end.transform.position;

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
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speedPlayer * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speedPlayer * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speedPlayer * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speedPlayer * Time.deltaTime);
        }

        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(horizontalInput, verticalInput, 0).normalized;
        //transform.Translate(movement * speedPlayer * Time.deltaTime);

        //2
        //while (Vector3.Distance(transform.position, end.transform.position) < distance)
        //{
        //    transform.position = start.transform.position;
        //}

        //3
        transform.position = Vector3.MoveTowards(transform.position, target, speedPlayer * Time.deltaTime);

        if (Vector2.Distance(transform.position, end1.transform.position) < 0.1f)
        {
            target = end2.transform.position;
        }
        if (Vector2.Distance(transform.position, end2.transform.position) < 0.1f)
        {
            target = end.transform.position;
        }
        if (Vector2.Distance(transform.position, end.transform.position) < 0.1f)
        {
            target = start.transform.position;

        }
        if (Vector2.Distance(transform.position, start.transform.position) < 0.1f)
        {
            target = end1.transform.position;

        }


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
        //if(timeLoop -1 > 0.1f)
        //{
        //    IsMoving = !IsMoving;
        //    timeLoop = 0f;
        //}
        //if (IsMoving)
        //{

        //    //transform.position = Vector3.Lerp(transform.position, target,speedPlayer);
        //    transform.position = Vector3.MoveTowards(transform.position, target, speedPlayer * Time.deltaTime);
        //}
        //else
        //    {
        //    rb.velocity = Vector2.zero;
        //    }

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
}
