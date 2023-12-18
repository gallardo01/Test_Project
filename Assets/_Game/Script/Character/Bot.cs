using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Bot : Character
{

    [SerializeField] private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        ChangAnim(Constants.ANIM_IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
