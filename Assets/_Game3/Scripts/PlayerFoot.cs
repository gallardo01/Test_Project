using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour
{
    [SerializeField] private Transform brickHolder;
    [SerializeField] private Vector3 nextPosition;
    [SerializeField] private PlayerController mainPlayer;
    // Start is called before the first frame update
    void Start()
    {
        // nextPosition = brickHolder.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Brick"){
            // Debug.Log(other.gameObject.GetComponent<Brick>().colorType);

            if((int)mainPlayer.colorType == (int)other.gameObject.transform.parent.gameObject.GetComponent<Brick>().colorType){
                // other.gameObject.GetComponent<BoxCollider>().enabled = false;
                Stage.Ins.RemoveBrick(other.gameObject.transform.parent.gameObject.GetComponent<Brick>());
                other.gameObject.transform.parent.transform.SetParent(brickHolder, false);
                other.gameObject.transform.parent.transform.localPosition = nextPosition;
                nextPosition += new Vector3(0,0.16f,0);
            }
            // transform.parent.gameObject.GetComponent<PlayerController>().
        }
    }

}
