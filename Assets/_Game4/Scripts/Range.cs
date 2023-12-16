using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    [SerializeField] private Character character;
    [SerializeField] private const string BotTag = "bot";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {   if (other.tag == BotTag)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Range Attack");
                character.Attack();
            }
        }
    }       
}
