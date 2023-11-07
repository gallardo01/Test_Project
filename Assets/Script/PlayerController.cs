using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Star;
    public GameObject end;
    private float speed = 1;
   // private bool isRunning = true; 
    private int step = 0;
    private GameObject[] pathRunning;
    private GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        pathRunning = gameController.GetComponent<GameController>().returnPath();
    }

    //IEnumerator stopPerSeconds()
    //{
    //    yield return new WaitForSeconds(Random.Range(1f,2f));
    //    speed = 1 - speed;
    //    isRunning = true;
    //    //StartCoroutine(stopPerSeconds());
    //}

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, pathRunning[step].transform.position, 0.01f * speed);

        if ( transform.position == pathRunning[step].transform.position)
        {
            step++; 
            if ( step == 5)
            {
                gameController.GetComponent<GameController>().increatPoint(1);
                gameObject.SetActive(false);
            }
        }
        //if (transform.position == Star.transform.position && isRunning)
        //{
        //    isRunning = false;
        //    StartCoroutine(stopPerSeconds());
        //}

        //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), Mathf.Infinity))
        //    {
        //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) , Color.yellow);
        //        Debug.Log("Did Hit");
        //    }
        //    else
        //    {
        //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        //        Debug.Log("Did not Hit");
        //    }

        //}
        //if (Input.GetKey("w"))
        //{
        //    //đi lên
        //    transform.Translate(Vector3.up * Time.deltaTime * speed); 
        //}
        //if (Input.GetKey("s")) 
        //{
        //    //đi xuống 
        //    transform.Translate(Vector3.down * Time.deltaTime * speed);
        //}
        //    //rẽ phải 
        //if (Input.GetKey("d")) 
        //{
        //    transform.Translate(Vector3.right * Time.deltaTime * speed);
        //}
        //    //rẽ trái
        //if (Input.GetKey("a")) 
        //{
        //    transform.Translate(Vector3.left * Time.deltaTime * speed); 
        //}

        //tọa độ
        /*   if ( absNumber(gameObject.transform.position.x , end.transform.position.x) < 0.5f
               && absNumber(gameObject.transform.position.y , end.transform.position.y) < 0.5f )
           {
               //gán vị trí 
               gameObject.transform.position = start.transform.position;
           }
        */
    }
    private float absNumber(float x, float y)
    {
        if ( x - y > 0 )
        {
            return x - y; 
        }
        return y - x; 
    }

    // lan dau tien khi va cham 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            gameObject.SetActive(false);
        }
    }

    // update - co dieu kien 2 vat va cham 
    void OnTriggerStay2D(Collider2D other)
    {

    } 

    //1 lan khi ma k va cham
    void OnTriggerExit2D(Collider2D other)
    {

    }
}
