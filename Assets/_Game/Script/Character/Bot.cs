using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{

    [SerializeField] private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        changColor((ColorType)Random.Range(1, 6));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
