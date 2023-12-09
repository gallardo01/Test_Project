using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    private Vector3 destionation;

    public bool IsDestintion => Vector3.Distance(destionation, transform.position.x * Vector3.right +
        Vector3.up * 2.788824f + Vector3.forward * transform.position.z) < 0.3f;

    public float distance => Vector3.Distance(destionation, transform.position.x * Vector3.right +
        Vector3.up * 3f + Vector3.forward * transform.position.z);
    // Start is called before the first frame update
    void Start()
    {
        destionation = transform.position;
        //changeAnim("idle");
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 2.788824f;
        agent.SetDestination(position);
        Debug.Log("Destination:" + position);
    }

    IState<Bot> currentState;
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
            CanMove(transform.position);
        }
    }
    
    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

}
