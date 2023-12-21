using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    private Vector3 destionation;

    public bool IsDestintion => (Mathf.Abs(destionation.x - transform.position.x) + Mathf.Abs(destionation.z - transform.position.z)) < 0.05f;

    private CounterTime counter = new CounterTime();
    public CounterTime Counter => counter;


    // Start is called before the first frame update
    void Start()
    {
        ChangeState(new PatrolState());
        //changeAnim("idle");
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 0.5f;
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
