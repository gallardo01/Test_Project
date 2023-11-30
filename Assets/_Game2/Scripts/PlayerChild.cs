using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerChild : MonoBehaviour
{
    [SerializeField] private Player parents;
    [SerializeField] private Transform lowerHalf, brickHolder;
    [SerializeField] private GameObject BrickInBrickHolder;
    private Vector3 nextPosition, aBrick = new Vector3(0f,0.25f, 0f);
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
        Debug.Log("Hi");
        if(other.tag == "Brick")
        {
            other.gameObject.transform.parent.transform.SetParent(brickHolder, false);
            other.gameObject.transform.parent.transform.localPosition = nextPosition;
            nextPosition += aBrick;
            // GameObject brick = Instantiate(BrickInBrickHolder, currentTransform.position + brickCount * new Vector3(0f, 0.25f), BrickInBrickHolder.transform.rotation, brickHolder);
            // brickCount++;
            // listBrick.Add(brick);
            //other.gameObject.transform.position = ;
            // Instantiate(brick, brickHolder);
            // Destroy(other.gameObject);
            //PlayerPrefs.GetInt("coin", coin);
            //UiManager.instance.SetCoin(coin);
        }
    }
}
