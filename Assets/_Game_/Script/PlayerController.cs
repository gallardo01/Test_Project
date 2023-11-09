using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 1;
    public Vector3 startPoint, endPoint;
    public  GameObject StartO, EndO;
    private GameObject[] path;
    private int step = 0;
    public GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        path = gameController.GetComponent<GameController>().returnPath();

        startPoint = transform.position;
        endPoint = new Vector3(0, -1.5f, 0);
        // StartCoroutine(stopRun());
    }

    // làm hàm lặp chạy 1s dừng 1s
    // private IEnumerator stopRun(){
    //     // speed = 1;
    //     yield return new WaitForSeconds(1f);
    //     speed = 1 - speed;
    //     // yield return new WaitForSeconds(1f);
    //     StartCoroutine(stopRun());
    // }

    // Update is called once per frame
    void Update()
    {   
        // transform.position = Vector3.MoveTowards(transform.position, path[step].transform.position, 0.01f * speed);

        transform.position = Vector3.MoveTowards(transform.position, path[step].transform.position, 0.01f * speed);
        if (transform.position == path[step].transform.position)
        {
            step++;
            if (step >= path.Length)
            {
                Destroy(gameObject);
            }
        }
        // Đi theo path1

        // Đi random theo path1 hoặc path2


        // Nếu vị trí của player bằng với vị trí cuối
        // if ((transform.position-endPoint).sqrMagnitude < .01){
        //     Debug.Log("End");
        //     step++;
        //     }


        // Đi đến điểm random
        // if (transform.position == path[step].transform.position)
        // {
        //     step++;
        //     if (step >= path.Length)
        //     {
        //         step = Random.Range(0, 5);
        //         gameObject.transform.position = StartO.transform.position;
        //     }
        // }

        // transform.position = Vector3.MoveTowards(transform.position, PointA.transform.position, 0.01f);

        // if (Input.GetKey("w"))
        // {
        //     transform.Translate(Vector3.up * Time.deltaTime * speed);
        // }
        // if (Input.GetKey("a"))
        // {
        //     transform.Translate(Vector3.left * Time.deltaTime * speed);
        // }
        // if (Input.GetKey("s"))
        // {
        //     transform.Translate(Vector3.down * Time.deltaTime * speed);
        // }
        // if (Input.GetKey("d"))
        // {
        //     transform.Translate(Vector3.right * Time.deltaTime * speed);
        // }


        // if (transform.position.y < endPoint.y && transform.position.x < 0.5f && transform.position.x > -0.5f)
        // {
        //     transform.position = startPoint;
        // }


        // if (absNumber(gameObject.transform.position.x, EndO.transform.position.x) <= 1f &&
        //     absNumber(gameObject.transform.position.y, EndO.transform.position.y) <= 1f)
        //     {
        //         gameObject.transform.position = StartO.transform.position;
        //     }
    }

    private float absNumber(float x, float y){
        if (x - y > 0){
            return x - y;
        }
        return y - x;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.tag == "End")
        // {
        //     gameObject.transform.position = StartO.transform.position;
        // }
    }
}
