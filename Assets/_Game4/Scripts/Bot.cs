using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    private Vector3 destination;

    public bool IsDestination => Vector3.Distance(destination, transform.position.x * Vector3.right + 
                                                                Vector3.up * 2.788f+ 
                                                                Vector3.forward * transform.position.z) < 0.1f;
    public int targetBrick;

    public float distanceY;    
    // Start is called before the first frame update
    void Start()
    {
        
        destination = transform.position;
        ChangeAnim(AnimConstant.idleAnim);
    }

    // Update is called once per frame
    public void SetDestination(Vector3 position)
    {
        
        agent.enabled = true;
        destination = position;
        destination.y = 2.788f;
        agent.SetDestination(position);
        ChangeAnim(AnimConstant.runAnim);
        Debug.Log("Destination: " + position);
    }

    IState<Bot> currentState;
    private void Update(){
        if(planeStage != null){
            distanceY = transform.position.y - planeStage.position.y;
        }
        if(currentState != null){
            currentState.OnExecute(this);
            CanMove(transform.position);
        }
    }

    public void ChangeState(IState<Bot> state){
        if(currentState != null){
            currentState.OnExit(this);
        }
        currentState = state;
        if(currentState != null){
            currentState.OnEnter(this);
        }
    }
}