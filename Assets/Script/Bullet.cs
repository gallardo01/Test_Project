using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject startPoint, endPoint;
    public int mode = 0;
    public GameObject sword;
    // Start is called before the first frame update
    void Start()
    {
        if(mode == 0){
            transform.position = startPoint.transform.position;
        }
        else{
            transform.position = endPoint.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mode == 0){
            transform.position = Vector3.MoveTowards(transform.position, startPoint.transform.position, 0.01f);
            if(transform.position == startPoint.transform.position){
                // Destroy(gameObject);
            }
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, 0.01f);
            if(transform.position == endPoint.transform.position){
                // Destroy(gameObject);
            }
        }
    }

    public void InitBullet(int modeInt){
        mode = modeInt;
        if(mode == 0){
            sword.transform.rotation = Quaternion.Euler(180, 180, -90);
        }
    }



}
