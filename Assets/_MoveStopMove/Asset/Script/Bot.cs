using ChangeAnim;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    private Vector3 destination;
    public bool IsDestination => Vector3.Distance(destination, transform.position.x * Vector3.right + Vector3.up * 0 + Vector3.forward * transform.position.z) < 0.3f;
    private void Start()
    {
        destination = transform.position;
        ChangeState(new PatrolState());
        OnInit();
    }
    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destination = position;
        destination.y = 0; ;
        agent.SetDestination(position);
    }
    IState<Bot> currentState;
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
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
