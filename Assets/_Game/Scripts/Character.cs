using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private GameObject BrickPrefab;

    private int totalStack = 0;
    private List<GameObject> playerBricks = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnInit(){

    }

    public virtual void OnDespawned(){

    }

}
