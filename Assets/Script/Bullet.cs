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
            transform.position = new Vector3(startPoint.transform.position.x,startPoint.transform.position.y,startPoint.transform.position.z);
            rotateBullet(mode);
        }
        else{
            transform.position = new Vector3(endPoint.transform.position.x,endPoint.transform.position.y,endPoint.transform.position.z);
            rotateBullet(mode);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mode == 0){
            transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, 0.01f);
            if(transform.position == endPoint.transform.position){
                // Destroy(gameObject);
                InitBullet(1);
                rotateBullet(mode);
            }
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, startPoint.transform.position, 0.01f);
            if(transform.position == startPoint.transform.position){
                // Destroy(gameObject);
                InitBullet(0);
                rotateBullet(mode);
            }
        }
    }

    private void rotateBullet(int modeInt) {
        if(modeInt == 0){
            sword.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else{

            sword.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
    public void InitBullet(int modeInt){
        mode = modeInt;
        // if(mode == 0){
        //     Debug.Log("Heo");
        // }
    }



}
