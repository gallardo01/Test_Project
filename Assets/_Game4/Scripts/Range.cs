using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    public bool enterRange = false, onTarget = false;
    public Vector3? target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag.characterTag)
        {
            onTarget = true;
            target = other.gameObject.transform.position;
            if(other.gameObject.GetComponent<Bot>() != null && Random.Range(0,100) % 10 == 0 && !other.gameObject.GetComponent<Character>().isAttack){
                other.gameObject.GetComponent<Bot>().isAttack = true;
                other.gameObject.GetComponent<Bot>().Rotate();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {   
        if (other.tag == Tag.characterTag)
        {
            Debug.Log("Target = True");
            onTarget = true;
            target = other.gameObject.transform.position;
            if(other.gameObject.GetComponent<Bot>() != null && Random.Range(0,100) % 10 == 0 && !other.gameObject.GetComponent<Character>().isAttack){
                other.gameObject.GetComponent<Bot>().isAttack = true;
                other.gameObject.GetComponent<Bot>().Rotate();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == Tag.characterTag)
        {
            Debug.Log("Target = False");
            onTarget = false;
            target = null;
        }
    }
}
