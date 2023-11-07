using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public GameObject startPoint;
    public GameObject[] path1;
    public GameObject[] path2;
    private int mode;
    private int step = 0;

    // public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        mode = Random.Range(0, 2);
        // gameController = GameObject.FindGameObjectWithTag("GameController");
        // path1 = gameController.GetComponent<gameController>().returnPath();
    }

    // Update is called once per frame
    void Update()
    {
        if(mode == 0){
            transform.position = Vector3.MoveTowards(transform.position, path1[step].transform.position, 0.02f);
            if(transform.position == path1[step].transform.position){
                step++;
                if(step >= path1.Length){
                    // step = 0;
                    // transform.position = startPoint.transform.position;
                    Destroy(gameObject);
                }
            }
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, path2[step].transform.position, 0.02f);
            if(transform.position == path2[step].transform.position){
                step++;
                if(step >= path2.Length){
                    // step = 0;
                    // transform.position = startPoint.transform.position;
                    Destroy(gameObject);
                    // GameController.gameController.score++;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet"){
            // Destroy(other.gameObject);
            Destroy(gameObject);
            GameController.gameController.score++;
        }
    }

}
