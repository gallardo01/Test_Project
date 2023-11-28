using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;


    public Transform BrickPref;
    public Transform BoxBrick;

    List<Transform> ListBrickPref = new List<Transform>();
   
    private void Start()
    {
        
    }
    void Update()
    {
        transform.position += JoytickController.direct * moveSpeed * Time.deltaTime;      
    }


    public void addBrick()
    {
        int index = ListBrickPref.Count;
        Transform playerBrick = Instantiate(BrickPref, BoxBrick);
        playerBrick.localPosition = Vector3.back*0.5f + index * 0.3f * Vector3.up + Vector3.up * 1.5f;
        ListBrickPref.Add(playerBrick);
    }

    public void removeBrick()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            //Debug.Log("collect");
            addBrick();
            Destroy(other.gameObject);
        }
    }
}
