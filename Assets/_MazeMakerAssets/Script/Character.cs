using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public Transform playerBody;
    [SerializeField] public Transform brickParent;

    public int totalStack = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void OnInit()
    {
    }

    public virtual void OnDespawn()
    {
    }
}
